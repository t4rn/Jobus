using Autofac;
using FluentValidation;
using System;

namespace Jobus.Core.Services.Validations
{
    public class FluentValidatorFactory : IValidatorFactory
    {
        private readonly IComponentContext _componentContext;

        public FluentValidatorFactory(IComponentContext container)
        {
            _componentContext = container;
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            var genericType = typeof(IValidator<>).MakeGenericType(type);
            object validator;
            if (_componentContext.TryResolve(genericType, out validator))
                return (IValidator)validator;

            return null;
        }
    }
}
