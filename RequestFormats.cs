namespace AccountCreation
{
    internal class AccountCreationRequest
    {
        public string accountKey { get; set; }
        public AccountCreationRequest(string accountKey)
        {
            this.accountKey = accountKey;
        }
    }

    internal class AccountCreationResponse
    {
        public int address { get; set; }
    }
}