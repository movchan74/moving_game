//using Boo.Lang.Runtime;
using HyperCasual.Extensions;
using UnityEngine;

namespace HyperCasual.Test
{
    /// <summary>
    /// Tests the RemoveChar string extension.
    /// </summary>
    [ExecuteInEditMode]
    public class TestRemoveCharExtensions
        : MonoBehaviour
    {
        public bool RunTest;

        public void OnDrawGizmos()
        {
            if (!RunTest)
                return;

            RunTest = false;
            const string input = "this is a piece of hudText written for the test";
            const string expected = "his is a piece of ex wrien for he es";
            var result = input.RemoveChar('t');

//            if (expected != result)
//                throw new AssertionFailedException(string.Format("Expected::{0}\nResult::{1}", expected, result));

            Debug.LogFormat("Input::{0}\nResult::{1}", input, result);
        }
    }
}
