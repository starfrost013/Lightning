using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Polaris.UI
{
    public class TextChunkCollection : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator(); 
        
        }


    public TextChunkCollectionEnumerator GetEnumerator()
        {
            return new TextChunkCollectionEnumerator(); 
        }
    }

    public class TextChunkCollectionEnumerator : IEnumerator
    {

    }
}
