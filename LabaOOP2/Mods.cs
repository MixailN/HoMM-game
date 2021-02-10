using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace LabaOOP2
{
    class Mods
    {
        public static List<Unit> LoadAllUnits()
        {
            List<Unit> units = new List<Unit>(GetUnitsFrom(GetTypesFromThis()));
            units.AddRange(GetUnitsFrom(GetTypesFromMods()));
            return units;
        }
        private static Unit[] GetUnitsFrom(Type[] types)
        {
            List<Unit> units = new List<Unit>();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract)
                {
                    units.Add((Unit)Activator.CreateInstance(type));
                }
            }
            return units.ToArray();
        }
        private static Type[] GetTypesFromThis()
        {
            return Assembly.GetExecutingAssembly().GetTypes();
        }
        private static Type[] GetTypesFromMods()
        {
            List<Type> types = new List<Type>();
            foreach (var path in Directory.GetFiles("Mods\\"))
            {
                Assembly qwer = Assembly.LoadFrom(path);
                types.AddRange(qwer.GetTypes());
            }
            return types.ToArray();
        }
        
    }
}
