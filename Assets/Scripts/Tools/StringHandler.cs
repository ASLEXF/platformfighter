using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringHandler : MonoBehaviour
{
    public static string ToSentence(string input)
    {
        string result = char.ToUpper(input[0]) + input.Substring(1);
        return Regex.Replace(result, @"([A-Z]|[0-9])", " $1").TrimStart();
    }
}
