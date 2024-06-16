using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Lib.Helpers;

public class GeneratePass {

    public static string CreateRandomPassword(int length) {

        string allowed = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";

        Random randNum = new Random();
        char[] chars   = new char[length];

        for (int i = 0; i < length; i++) {
            chars[i] = allowed[(int)(allowed.Length * randNum.NextDouble())];
        }

        return new string(chars);
    }
}