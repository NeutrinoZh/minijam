using System;

namespace Game
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SpellCasterAttribute : Attribute
    {
        public Type SpellType;
    }
}