using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace DynamicLoopAutoMapper.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(configuration =>
                                  {
                                      var profiles = typeof(AutoMapperConfiguration).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
                                      foreach (var profile in profiles)
                                      {
                                          configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
                                      }

                                  });
        }
    }
}