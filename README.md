# unity-solana-demo
Demonstration of Unity-Solana Wallet in action

## Screens explanation
### Login Screen

![Screenshot_3](https://user-images.githubusercontent.com/58888833/150561580-2a98e9d4-dedb-4909-a200-e469e6d3054f.png)

If you have already logged in to your wallet, then your mnemonics are stored in memory and you can log in with a password.
Otherwise you have to create or restore a wallet.

### Create Wallet Screen

![Screenshot_4](https://user-images.githubusercontent.com/58888833/150562094-9da48a91-30d4-433f-8d8c-61d54bd84d89.png)

You now have automatically generated mnemonics and to successfully create a wallet you must enter a password ith which the mnemonics will be encrypted.
I recommend that you use the Save Mnemonics option and save them to a text file.
Then press create button to create a wallet.

### Regenerate Wallet Screen

![Screenshot_5](https://user-images.githubusercontent.com/58888833/150563186-21fc2af0-d900-44b3-b371-ab97fe6730ff.png)

If you have saved mnemonics and want to recreate a wallet with it,load them by pressing the load mnemonics and generate the password again.
Now your wallet is regenerated.

### Wallet Screen

![Screenshot_6](https://user-images.githubusercontent.com/58888833/150563680-51a62ecb-bd28-4293-a71a-1a2628569a80.png)

If you have successfully logged in / generated / regenerated a wallet you will automatically be transferred to the wallet screen.
Now you are shown SOL balance and your NFT's.
After successfully logging in to the wallet you are automatically subscribed to the account via the websocket. This allows you to track changes in your account (automatic refreshing of SOL balance when a balance changes, etc..).

### Recieve Screen 

![Screenshot_2](https://user-images.githubusercontent.com/58888833/150565477-e971c5ec-030f-4d73-b547-5adadf30e08d.png)

To facilitate testing, there is an Airdfrop option in the Recieve section. Click on the Airdrop button, return to the Wallet Screen and wait a few seconds to see the change in SOL balance.

### Transfer Screen

![Screenshot_7](https://user-images.githubusercontent.com/58888833/150565963-c5c100ce-94c8-4444-a990-ac952646f966.png)

To complete the transaction enter the wallet pubkey and the amount you want to send.
Then return to the wallet screen and wait a few seconds for the SOL Balance to refresh.

