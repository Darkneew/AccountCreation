using System.Net.Http.Json;
using System.Threading.Tasks;
using Chaos.NaCl;

namespace WebAPIClient
{
    class Program
    {
        static private readonly HttpClient client = new HttpClient();
        static private byte[] seedBytes = new byte[32];
        static private byte[] publicKeyBytes = new byte[32];
        static private byte[] privateKeyBytes = new byte[64];
        static private Random rdm; 

        static async Task Main(string[] args)
        {
            rdm = new Random();
            rdm.NextBytes(seedBytes);
            Ed25519.KeyPairFromSeed(publicKeyBytes, privateKeyBytes, seedBytes);

            string seed = Convert.ToHexString(seedBytes);
            string publicKey = Convert.ToHexString(publicKeyBytes);
            string privateKey = Convert.ToHexString(privateKeyBytes);
            int address = await CreateAccount(publicKey);
            Console.WriteLine("Your account has been created, at the address #{0}!\n\n", address);
            Console.WriteLine("Your seed is: {0}", seed);
            Console.WriteLine("Your private key is: {0}", privateKey);
            Console.WriteLine("Your public key is: {0}", publicKey);
            Console.WriteLine("Make sure to write them down. If you loose your private key, no one will be able to help you.");
            Console.WriteLine("\n\nPress enter to quit");
            Console.Read();
        }

        private static async Task<int> CreateAccount(string publicKey)
        {
            HttpResponseMessage response =
                await client.PostAsJsonAsync("https://convex.world/api/v1/createAccount",
                    new AccountCreation.AccountCreationRequest(publicKey));
            response.EnsureSuccessStatusCode();

            AccountCreationResponse data = await response.Content.ReadFromJsonAsync<AccountCreationResponse>();
            return data.address;
        }
    }
}

