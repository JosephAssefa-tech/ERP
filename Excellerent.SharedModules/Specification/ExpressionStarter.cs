using System;
using System.Linq.Expressions;

namespace Excellerent.SharedModules.Specification
{
    public class ExpressionStarter<T>
    {
        public Expression<Func<T, bool>> DefaultExpression { get; set; }
        internal ExpressionStarter() : this(false) { }
        private Expression<Func<T, bool>> Predicate => (IsStarted || !UseDefaultExpression) ? _predicate : DefaultExpression;

        private Expression<Func<T, bool>> _predicate;
        public bool IsStarted => _predicate != null;
        public bool UseDefaultExpression => DefaultExpression != null;

        internal ExpressionStarter(bool defaultExpression)
        {
            if (defaultExpression)
                DefaultExpression = f => true;
            else
                DefaultExpression = f => false;
        }
        /// <summary>Set the Expression predicate</summary>
        /// <param name="exp">The first expression</param>
        public Expression<Func<T, bool>> Start(Expression<Func<T, bool>> exp)
        {
            if (IsStarted)
                throw new Exception("Predicate cannot be started again.");

            return _predicate = exp;
        }
        internal ExpressionStarter(Expression<Func<T, bool>> exp) : this(false)
        {
            _predicate = exp;
        }
        public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> expr2)
        {
            return (IsStarted) ? _predicate = Predicate.Or(expr2) : Start(expr2);
        }
        /// <summary>And</summary>
        public Expression<Func<T, bool>> And(Expression<Func<T, bool>> expr2)
        {
            return (IsStarted) ? _predicate = Predicate.And(expr2) : Start(expr2);
        }
        public static implicit operator Expression<Func<T, bool>>(ExpressionStarter<T> right)
        {
            return right == null ? null : right.Predicate;
        }
        public static implicit operator Func<T, bool>(ExpressionStarter<T> right)
        {
            return right == null ? null : (right.IsStarted || right.UseDefaultExpression) ? right.Predicate.Compile() : null;
        }

        /// <summary>
        /// Allows this object to be implicitely converted to an Expression{Func{T, bool}}.
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator ExpressionStarter<T>(Expression<Func<T, bool>> right)
        {
            return right == null ? null : new ExpressionStarter<T>(right);
        }
    }
}
