﻿namespace PGE.Util
{
    public class ListItem
    {
        private string _Text;

        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private object _Value;

        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public ListItem(string text, object value)
        {
            _Text = text;
            _Value = value;
        }
    }
}
