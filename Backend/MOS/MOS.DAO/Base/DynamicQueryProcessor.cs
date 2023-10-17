using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.Base
{
    internal class DynamicQueryProcessor<TSource>
    {
        private List<MemberInfo> members = new List<MemberInfo>();

        public DynamicQueryProcessor<TSource> Add<TValue>(Expression<Func<TSource, TValue>> selector)
        {
            var member = ((MemberExpression)selector.Body).Member;
            members.Add(member);
            return this;
        }

        public void SetMemberInfo(List<string> Columns)
        {
            Type sourceType = typeof(TSource);
            foreach (var columnName in Columns)
            {
                MemberInfo[] mInfos = sourceType.GetMember(columnName);
                members.AddRange(mInfos);
            }
        }

        public IQueryable<TResult> Select<TResult>(IQueryable<TSource> source)
        {
            var sourceType = typeof(TSource);
            var resultType = typeof(TResult);
            var parameter = Expression.Parameter(sourceType, "e");
            var bindings = members.Select(member => Expression.Bind(
                resultType.GetProperty(member.Name), Expression.MakeMemberAccess(parameter, member)));
            var body = Expression.MemberInit(Expression.New(resultType), bindings);
            var selector = Expression.Lambda<Func<TSource, TResult>>(body, parameter);
            return source.Select(selector);
        }
    }
}
