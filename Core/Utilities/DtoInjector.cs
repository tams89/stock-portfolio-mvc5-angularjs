using System.Collections.Generic;
using System.Linq;
using Core.Factory;
using Omu.ValueInjecter;

namespace Core.Utilities
{
    public class DtoInjector<T, DtoT> where DtoT : class, new()
    {
        public static IEnumerable<DtoT> InjectList(IEnumerable<T> injectFrom)
        {
            var dto = DtoFactory<DtoT>.Create();
            return injectFrom.AsParallel().AsOrdered().Select(obj => dto.InjectFrom(obj) as DtoT).ToList();
        }

        public static DtoT Inject(object injectFrom)
        {
            var dto = DtoFactory<DtoT>.Create();
            var injectedDto = dto.InjectFrom(injectFrom) as DtoT;
            return injectedDto;
        }
    }
}
