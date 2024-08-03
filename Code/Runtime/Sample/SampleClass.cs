using Debugging.Runtime.Core;
using UnityEngine;

namespace Debugging.Runtime.Sample
{
    public class SampleClass : CommandMonoBehaviour
    {
        [Command("/hello")]
        private void SayHello()
        {
            Debug.Log("Hello..2.");
        }
    }
}