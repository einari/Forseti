using System.Linq.Expressions;
using System.Reflection;

namespace Forseti.Extensions
{
	/// <summary>
	/// Provides methods for working with expressions
	/// </summary>
	public static class ExpressionExtensions
	{
		/// <summary>
		/// Get <see cref="MethodInfo">MethodInfo</see> from an <see cref="Expression">expression</see> - if any
		/// </summary>
		/// <param name="expression"><see cref="Expression">Expression</see> to get MethodInfo from</param>
		/// <returns>The <see cref="MethodInfo">MethodInfo</see> found, null if did not find one</returns>
		public static MethodInfo GetMethodInfo(this Expression expression)
		{
			var lambda = expression as LambdaExpression;
			if (null != lambda &&
			    lambda.Body is MethodCallExpression)
			{
				var methodCall = lambda.Body as MethodCallExpression;
				return methodCall.Method;
			}
			return null;
		}


		/// <summary>
		/// Get <see cref="MemberExpression">MemberExpression</see> from an <see cref="Expression">expression</see> - if any
		/// </summary>
		/// <param name="expression"><see cref="Expression">Expression</see> to get <see cref="MemberExpression">MemberExpression</see> from</param>
		/// <returns><see cref="MemberExpression">MemberExpression</see> instance, null if there is none</returns>
		public static MemberExpression GetMemberExpression(this Expression expression)
		{
			var lambda = expression as LambdaExpression;
			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = lambda.Body as UnaryExpression;
				memberExpression = unaryExpression.Operand as MemberExpression;
			}
			else
			{
				memberExpression = lambda.Body as MemberExpression;
			}
			return memberExpression;
		}

		/// <summary>
		/// Get <see cref="FieldInfo">FieldInfo</see> from an <see cref="Expression">Expression</see> - if any
		/// </summary>
		/// <param name="expression"><see cref="Expression">Expression</see> to get <see cref="FieldInfo">FieldInfo</see> from</param>
		/// <returns><see cref="FieldInfo">FieldInfo</see> instance, null if there is none</returns>
		public static FieldInfo GetFieldInfo(this Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);
			var fieldInfo = memberExpression.Member as FieldInfo;
			return fieldInfo;
		}

		/// <summary>
		/// Get <see cref="PropertyInfo">PropertyInfo</see> from an <see cref="Expression">Expression</see> - if any
		/// </summary>
		/// <param name="expression"><see cref="Expression">Expression</see> to get <see cref="PropertyInfo">PropertyInfo</see> from</param>
		/// <returns><see cref="PropertyInfo">PropertyInfo</see> instance, null if there is none</returns>
		public static PropertyInfo GetPropertyInfo(this Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);
			var propertyInfo = memberExpression.Member as PropertyInfo;
			return propertyInfo;
		}

		/// <summary>
		/// Get an instance reference from an <see cref="Expression">Expression</see> - if any
		/// </summary>
		/// <param name="expression"><see cref="Expression">Expression</see> to get an instance from</param>
		/// <returns>The instance, null if there is none</returns>
		public static object GetInstance(this Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);
			var constantExpression = memberExpression.Expression as ConstantExpression;
			if (null == constantExpression)
			{
				
				var innerMember = memberExpression.Expression as MemberExpression;
				constantExpression = innerMember.Expression as ConstantExpression;
				if( null != innerMember && innerMember.Member is PropertyInfo )
				{
					var value = ((PropertyInfo) innerMember.Member).GetValue(constantExpression.Value, null);
					return value;
				}
			}
			return constantExpression.Value;
			
		}

		/// <summary>
		/// Get an instance reference from an <see cref="Expression">Expression</see>, with a specific type - if any
		/// </summary>
		/// <typeparam name="T">Type of the instance</typeparam>
		/// <param name="expression"><see cref="Expression">Expression</see> to get an instance from</param>
		/// <returns>The instance, null if there is none</returns>
		public static T GetInstance<T>(this Expression expression)
		{
			return (T)GetInstance(expression);
		}
	}
}