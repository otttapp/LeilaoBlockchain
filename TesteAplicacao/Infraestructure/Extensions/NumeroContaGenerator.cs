namespace TesteAplicacao.Infraestructure.Extensions
{
    public static class NumeroContaGenerator
    {
        private static readonly Random _random = new();

        public static string GerarNumeroConta()
        {
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string parteLetras = new string(Enumerable.Range(0, 3)
                .Select(_ => letras[_random.Next(letras.Length)]).ToArray());

            string parteNumeros = _random.Next(0, 10000).ToString("D4");

            return $"{parteLetras}{parteNumeros}";
        }
    }

}
