// See https://aka.ms/new-console-template for more information
Console.WriteLine("* - delets symbol, # - stops string");
bool toQuit = false;
do
{
    var input = ReadConsole();
    Console.WriteLine($"Original string was {input}");
    Console.WriteLine($"Type 'quit' and press 'enter' to quit application or press 'enter' to continue to new translation");
    toQuit = Console.ReadLine()?.ToLower() == "quit";
}
while (!toQuit);
///
char buttonPhoneString(List<char> originalString)
{
    int index = (originalString[0] - '0') - 2;
    int basicShift = index * 3;
    int shiftChar = (index > 5 ? basicShift + 1 : basicShift) + originalString.Count + 'A' - 1;
    return (char)shiftChar;
}
///
string ReadConsole()
{
    string retValue = ""; 
    List<char> tempString = [];
    ConsoleKeyInfo keyInfo;
    Int64 systTime = DateTime.Now.Millisecond;
    bool newSymbol = false;
    int limit;
    do
    {
        keyInfo = Console.ReadKey(intercept: true);

        if((keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9' && keyInfo.KeyChar != '1') || keyInfo.KeyChar == '*')
        {
            limit = keyInfo.KeyChar == '9' || keyInfo.KeyChar == '7' ? 4 : 3;
            if (keyInfo.KeyChar == '*')
            {
                if(retValue.Length > 0)
                    retValue = retValue[0..^1];
                if(tempString.Count > 0)
                    tempString = tempString[0..^1];
                Console.Write("\b \b");
            }
            else if(keyInfo.KeyChar == '0')
            {
                Console.Write(' ');
                tempString.Clear();
            }
            else
            {
                int delta = (new TimeSpan(DateTime.Now.Ticks - systTime)).Seconds;
                newSymbol = tempString.Count == 0 ||
                    (delta > 1 && tempString.Count > 0 && tempString[^1] == keyInfo.KeyChar) ||
                    (tempString.Count > 0 && tempString[^1] != keyInfo.KeyChar) ;
                /// ТОлько для выполнения условия по отображению оригинальной строки с пробелом при переходе на другую букву на той же цифре 
                if (delta > 1)
                    retValue += " ";
                ///
                if (!newSymbol)
                {
                    Console.Write("\b \b");
                    if(limit <= tempString.Count)
                    {
                        tempString.Clear();
                    }
                    tempString.Add(keyInfo.KeyChar);
                }
                else
                {
                    tempString.Clear();
                    tempString.Add(keyInfo.KeyChar);
                }
                Console.Write(buttonPhoneString(tempString));
            }
        }
        systTime = DateTime.Now.Ticks;
        retValue += keyInfo.KeyChar;
    }
    while (keyInfo.KeyChar != '#');
    Console.WriteLine();
    return retValue;
}