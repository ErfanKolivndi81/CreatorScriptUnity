using StringBuilder = System.Text.StringBuilder;

namespace IRK.Unity3D.CreatorScript
{
    public class MessageUnity
    {
        //Parameters:
        //In this way,both consecutive items are a parameter,
        //the first is the type of the parameter and the second is the name of the parameter
        public MessageUnity(string key, int group, params string[] parameters)
        {
            _key = key;
            _group = group;

            _parameters = new Parameter[parameters.Length / 2];
            for (int i = 0, s = 0; i < _parameters.Length; i++, s += 2)
            {
                _parameters[i] = new Parameter()
                {
                    type = parameters[s],
                    name = parameters[s + 1]
                };
            }

            CreateCode();
            CreateLable();
        }

        private class Parameter
        {
            public string type;
            public string name;
        }

        private Parameter[] _parameters;
        private string _code;
        private string _lable;
        private string _key;
        private int _group;

        public ScriptType type;

        public string key { get { return _key; } }
        public string code { get { return _code; } }
        public string lable { get { return _lable; } }
        public int indexGroup { get { return _group; } set { _group = value; } }
        public GroupMessageType groupMessage { get { return (GroupMessageType)_group; } }

        private void CreateCode()
        {
            var result = new StringBuilder("private void ");

            result.Append(_key + "(");

            for (int i = 0; i < _parameters.Length; i++)
            {
                result.Append(_parameters[i].type + " " + _parameters[i].name);

                if (i == _parameters.Length - 1)
                    break;

                result.Append(",");
            }

            result.AppendLine(")");
            result.AppendLine("\t{");
            result.AppendLine("\t}");

            _code = result.ToString();
        }

        private void CreateLable()
        {
            StringBuilder result = new StringBuilder(_key);

            result.Append("(");

            for (int i = 0; i < _parameters.Length; i++)
            {
                result.Append(_parameters[i].type);

                if (i == _parameters.Length - 1)
                    break;

                result.Append(",");
            }

            result.Append(")");

            _lable = result.ToString();
        }

    };
}
