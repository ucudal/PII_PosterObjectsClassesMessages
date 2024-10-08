using System;
using System.Linq;

namespace Ucu.Poo.Persons;

public class IdUtils
{
    /// <summary>
    /// Valida un número de cédula de identidad; el número puede tener o no puntos y el guión pero debe tener el dígito verificador. Adaptado
    /// de https://es.wikipedia.org/wiki/C%C3%A9dula_de_Identidad_de_Uruguay#Match_on_Card.
    /// </summary>
    /// <param name="id">El número de cédula</param>
    /// <returns>Retorna true si el número de cédula es válido y false en caso contrario.</returns>
    public static bool IdIsValid(string id)
    {
        long tempOut;

        // Quitar puntos y guiones
        id = id.Replace(".", "");
        id = id.Replace("-", "");

        // Validar largo
        if (id.Length == 8 && long.TryParse(id, out tempOut))
        {
            var idAsCharArray = id.ToArray();
            var idAsIntArray = idAsCharArray.Select(c => int.Parse(c.ToString())).ToArray();
            var referenceArray = "2987634".ToArray().Select(c => int.Parse(c.ToString())).ToArray();
            var inputCheckDigit = idAsIntArray[7];

            // Calcular número verificador
            int checkSum = 0;
            for (int i = 0; i < referenceArray.Length; i++)
            {
                checkSum = checkSum + (idAsIntArray[i] * referenceArray[i]);
            }

            int checkDigit = 10 - (checkSum % 10);

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            if (checkDigit != inputCheckDigit)
            {
                /// Número verificador ingresado inválido
                return false;
            }
        }
        else
        {
            // Formato de cédula de identidad inválido
            return false;
        }

        return true;
    }
}

public static class Program
{
    /// <summary>
    /// Crea una persona y trata de asignar cédulas válidas e inválidas para
    /// mostrar qué sucede. Utilizando sólo lo que aprendimos hasta el momento,
    /// para cumplir con la consigna de que una persona sólo puede tener nombre
    /// y cédula válida es necesario: 1. Controlar que la cédula y el nombre
    /// sean válidos antes de crear una instancia de la clase Person. 2.
    /// Modificar los setter del nombre y la cédula de la clase Person para
    /// asignar el valor recibido sólo cuando el nombre y la cédula son válidos.
    /// El setter de la cédula debe usar la clase IdUtils que fue provista.
    /// </summary>
    public static void Main()
    {
        Person john = new Person("John Doe", "1.234.567-2");
        Person jane = new Person("Jane Doe", "8.765.432-7");
        john.IntroduceYourself();
        jane.IntroduceYourself();
    }
}
