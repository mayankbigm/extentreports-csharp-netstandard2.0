using System;

namespace AventStack.ExtentReports.Model
{
    [Serializable]
    public abstract class Attribute
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        protected Attribute(string name, string description)
        {
            Name = name;
            Value = description;
        }

        protected Attribute(string name) : this(name, null) { }
    }
}
