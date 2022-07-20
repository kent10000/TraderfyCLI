using System.Collections;

namespace Traderfy.TarkovSharp;

public struct TarkovRequestArgs 
{
    public string ArgumentName { get; }

    public dynamic ArgumentValue { get; }


    public TarkovRequestArgs(string argumentName, ItemType argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, ItemCategoryName argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, TraderName argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, ItemSourceName argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, RequirementType argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, StatusCode argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, ItemType[] argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, ItemCategoryName[] argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, TraderName[] argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, ItemSourceName[] argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, RequirementType[] argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, StatusCode[] argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, string argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    public TarkovRequestArgs(string argumentName, IEnumerable argumentValue)
    {
        ArgumentName = argumentName;
        ArgumentValue = argumentValue;
    }
    
}