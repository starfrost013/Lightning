using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.API
{
    public class AnimationFrameCollection : IEnumerable
    {
        public List<AnimationFrame> AnimationFrames { get; set; }

        public AnimationFrameCollection()
        {
            AnimationFrames = new List<AnimationFrame>();
        }

        public AnimationFrameCollection(List<AnimationFrame> Frames)
        {
            AnimationFrames = Frames; 
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)GetEnumerator();

        public AnimationFrameCollectionEnumerator GetEnumerator() => new AnimationFrameCollectionEnumerator(AnimationFrames);

        public AnimationFrame this[int i] => AnimationFrames[i];


    }

    public class AnimationFrameCollectionEnumerator : IEnumerator
    {
        public List<AnimationFrame> AnimationFrames { get; set; }

        private int Position = -1;

        public AnimationFrame Current
        {
            get
            {
                try
                {
                    return AnimationFrames[Position];
                }
                catch (IndexOutOfRangeException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("AnimationFrameCollection", "AttemptedToAcquireInvalidAnimationFrameException", $"Attempted to acquire invalid animation frame {Position}, maximum is {AnimationFrames.Count - 1}!\n\nException: {err}");
#else
                    ErrorManager.ThrowError("AnimationFrameCollection", "AttemptedToAcquireInvalidAnimationFrameException", $"Attempted to acquire invalid animation frame {Position}, maximum is {AnimationFrames.Count - 1}!");
#endif
                    return null;
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (object)Current; 
            }
        }
        public AnimationFrameCollectionEnumerator(List<AnimationFrame> Frames)
        {
            AnimationFrames = Frames;
        }

        public void Reset() => Position = -1;

        public bool MoveNext()
        {
            Position++;
            return (Position < AnimationFrames.Count);
        }
    }
}
