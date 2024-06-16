using System.Linq.Expressions;
using System.Reflection;

namespace Shared.Lib.Helpers;

public static class IncludesHelper {
    public static string[] GetIncludes<T>(params Expression<Func<T, object?>>[] expressions) {
        string[] includes = [];

        foreach(var expression in expressions) {
            if (expression.Body is UnaryExpression unex) {
                if (unex.NodeType == ExpressionType.Convert) {
                    Expression ex        = unex.Operand;
                    MemberExpression mex = (MemberExpression)ex;

                    includes = [.. includes, mex.Member.Name];
                }
            }

            MemberExpression memberExpression    = (MemberExpression)expression.Body;
            MemberExpression memberExpressionOrg = memberExpression;

            string Path = "";

            while (memberExpression.Expression.NodeType == ExpressionType.MemberAccess) {
                var propInfo  = memberExpression.Expression.GetType().GetProperty("Member");
                var propValue = propInfo.GetValue(memberExpression.Expression, null) as PropertyInfo;

                Path = propValue.Name + "." + Path;
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            includes = includes.Append(Path + memberExpressionOrg.Member.Name).ToArray();
        }

        return includes;
    }
}