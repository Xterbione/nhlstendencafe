namespace nhl_stenden_cafe.Pages.Repositorys
{
    public class GuidRepository
    {
        /// <summary>
        /// thanks to @https://www.codegrepper.com/code-examples/csharp/generate+random+long+string+c%23
        /// needed it to fix mysql not comparing c# generated unique id's
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
