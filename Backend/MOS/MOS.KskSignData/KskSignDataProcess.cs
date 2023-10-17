using MOS.SDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace MOS.KskSignData
{
    public class KskSignDataProcess
    {
        public static void ProcessSignData(KskSyncSDO ado, X509Certificate2 certificate)
        {
            try
            {
                if (certificate != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    System.Xml.Serialization.XmlRootAttribute root = new System.Xml.Serialization.XmlRootAttribute();
                    root.ElementName = "root";
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(ado.GetType(), root);
                    using (Stream xmlStream = new MemoryStream())
                    {
                        using (StreamWriter wr = new StreamWriter(xmlStream, Encoding.UTF8))
                        {
                            x.Serialize(wr, ado);
                            xmlStream.Position = 0;
                            xmlDocument.Load(xmlStream);
                        }
                    }

                    SignXml(xmlDocument, certificate);

                    using (var stringWriter = new StringWriter())
                    using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                    {
                        xmlDocument.WriteTo(xmlTextWriter);
                        xmlTextWriter.Flush();
                        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(stringWriter.GetStringBuilder().ToString());
                        ado.SIGNDATA = System.Convert.ToBase64String(plainTextBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        // Sign an XML file.
        // This document cannot be verified unless the verifying
        // code has the key with which it was signed.
        private static void SignXml(XmlDocument xmlDoc, X509Certificate2 cer)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException(null, "xmlDoc");
            if (cer == null)
                throw new ArgumentException(null, "cer");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            RSA rsaKey = ((RSA)cer.PrivateKey);
            signedXml.SigningKey = rsaKey;

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cer);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            #region object
            XmlDocument document = new XmlDocument();
            XmlNode properties = document.CreateNode(XmlNodeType.Element, "", "SignatureProperties", "");
            XmlNode property = document.CreateNode(XmlNodeType.Element, "", "SignatureProperty", "");

            XmlAttribute id = document.CreateAttribute("Id");
            property.Attributes.Append(id);
            id.Value = "SigningTime";

            XmlAttribute Target = document.CreateAttribute("Target");
            property.Attributes.Append(Target);
            Target.Value = "signatureProperties";

            XmlNode SigningTime = document.CreateNode(XmlNodeType.Element, "", "SigningTime", " ");

            SigningTime.InnerText = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");

            property.AppendChild(SigningTime);
            properties.AppendChild(property);

            document.AppendChild(properties);

            System.Security.Cryptography.Xml.DataObject obj = new System.Security.Cryptography.Xml.DataObject();
            obj.Data = document.ChildNodes;
            #endregion

            // Add the reference to the SignedXml object.
            signedXml.AddObject(obj);

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }
    }
}
