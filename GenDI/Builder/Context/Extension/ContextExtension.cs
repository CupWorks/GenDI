using System;
using System.Collections.Generic;
using System.Linq;

namespace GenDI.Builder.Context.Extension
{
    public static class ContextExtension
    {
        public static T AddOrCreate<T>(this Dictionary<string, T> dictionary, string name, Action<T>? builder) 
            where T : NameContextBase, new()
        {
            if (!dictionary.TryGetValue(name, out var context))
            {
                context = new T
                {
                    Name = name
                };
                dictionary.Add(name, context);
            }
            
            builder?.Invoke(context);
            return context;
        }
        
        public static T AddOrCreate<T>(this Dictionary<string, NameContextBase> dictionary, string name, Action<T>? builder) 
            where T : NameContextBase, new()
        {
            if (!dictionary.TryGetValue(name, out NameContextBase context))
            {
                context = new T
                {
                    Name = name
                };
                dictionary.Add(name, context);
            }
            
            builder?.Invoke((T)context);
            return (T)context;
        }
        
        public static void CreateAndAdd<T>(this List<T> list, Action<T>? builder) 
            where T : ContextBase, new()
        {
            var context = new T();
            list.Add(context);
            
            builder?.Invoke(context);
        }
        
        public static void CreateAndAdd<T>(this List<ContextBase> list, Action<T>? builder) 
            where T : ContextBase, new()
        {
            var context = new T();
            list.Add(context);
            
            builder?.Invoke(context);
        }
        
        public static void AddOrCreate<T>(this List<ContextBase> list, string name, Action<T>? builder) 
            where T : NameContextBase, new()
        {
            var context = list.OfType<T>()
                .FirstOrDefault(c => c.Name == name);

            if (context == null)
            {
                context = new T
                {
                    Name = name
                };
                list.Add(context);
            }
            
            builder?.Invoke(context);
        }
    }
}