﻿namespace Aparta.Authentication.Domain.Validation;

public class ValidateCPF
{
    public static bool IsValid(string cpf)
    {
        int[] multi1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multi2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        string tempCpf = cpf.Substring(0, 9);
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multi1[i];

        int rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        string digit = rest.ToString();
        tempCpf = tempCpf + digit;
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multi2[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cpf.EndsWith(digit);
    }
}
