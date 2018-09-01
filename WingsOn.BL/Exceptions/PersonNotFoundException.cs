﻿using System;

namespace WingsOn.BL
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException()
        {
        }

        public PersonNotFoundException(string message)
        : base(message)
        {
        }

        public PersonNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
