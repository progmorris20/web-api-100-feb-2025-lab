
# Early Bound Examples
```csharp

public static class Math 
{

    public static int Add(int a, int b)
    {
        return a + b;
    }
}

// elsewhere in my code

var sum = Math.Add(1,13);
System.Console.WriteLine(sum);



```


# Late Bound (Backing Services)

```csharp

var sum = await someApi.GetSumFor(1,2);

```