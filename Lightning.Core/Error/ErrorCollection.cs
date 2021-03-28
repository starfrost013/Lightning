using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lightning.Core
{
    /// <summary>
    /// ErrorCollection
    /// 
    /// Non-DataModel 
    /// 
    /// A collection of errors,
    /// </summary>
    public class ErrorCollection : IEnumerable
    {
        // each error
        // not sure if this is right
        [XmlElement("Errors")]
        public List<Error> ErrorList { get; set; }

        public ErrorCollection()
        {
            ErrorList = new List<Error>();
        }

        public ErrorCollection(List<Error> NewErrors)
        {

            if (NewErrors == null)
            {
                if (NewErrors.Count == 0)
                {
                    ErrorManager.ThrowError("Error Serialiser & Loader", new Error { Id = 0xDEAD5555, Name = "InvalidErrorException", Description = "Unknown error in serialisation: attempted to instantiate ErrorCollection with empty list of errors!", Severity = MessageSeverity.FatalError });
                }
                else
                {
                    // safety

                    ErrorList = new List<Error>();

                    foreach (Error Err in NewErrors)
                    {
                        ErrorList.Add(Err);
                    }
                }
            }
            else
            {
                ErrorManager.ThrowError("Error Serialiser & Loader", new Error { Id = 0xDEAD1111, Name = "InvalidErrorException", Description = "Unknown error in serialisation: attempted to instantiate ErrorCollection with invalid list of errors!", Severity = MessageSeverity.FatalError });
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ErrorCollectionEnumerator GetEnumerator()
        {
            return new ErrorCollectionEnumerator(ErrorList);
        }

    }

    public class ErrorCollectionEnumerator : IEnumerator
    { 
        private List<Error> Errors { get; set; }
        public int Position = -1;
        public Error Current { get; set; }

        object IEnumerator.Current
        {
            get
            {
                return (object)Current;
            }
        }

        public void Reset() => Position = -1; 
        public bool MoveNext()
        {
            Position++;
            Current = Errors[Position];
            return (Position < Errors.Count);
        }

        public ErrorCollectionEnumerator(List<Error> NewErrors)
        {
            Errors = new List<Error>();

            foreach (Error CurErr in NewErrors)
            {
                Errors.Add(CurErr);
            }
        }
    }

}
