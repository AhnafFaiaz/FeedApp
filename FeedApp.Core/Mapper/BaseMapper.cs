using AutoMapper;
using FeedApp.Core.Interfaces.IMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Mapper
{
    public class BaseMapper<TDestination, TSource> : IBaseMapper<TDestination, TSource>
    {
        private readonly IMapper _mapper;

        public BaseMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination MapModel(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public IEnumerable<TDestination> MapList(IEnumerable<TSource> source)
        {
            return _mapper.Map<IEnumerable<TDestination>>(source);
        }

        public TSource MapModel(TDestination source)
        {
            return _mapper.Map<TSource>(source); //PRB 
        }

        public IEnumerable<TSource> MapList(IEnumerable<TDestination> source)
        {
            return _mapper.Map<IEnumerable<TSource>>(source);
        }
    }
}
