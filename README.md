### Домашняя работа № 4. Рефлексия и её применение

1. Написать сериализацию свойств или полей класса в строку
2. Проверить на классе: class F { int i1, i2, i3, i4, i5; Get() => new F(){ i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; }
3. Замерить время до и после вызова функции (для большей точности можно сериализацию сделать в цикле 100-100000 раз)
4. Вывести в консоль полученную строку и разницу времен
5. Отправить в чат полученное время с указанием среды разработки и количества итераций
6. Замерить время еще раз и вывести в консоль сколько потребовалось времени на вывод текста в консоль
7. Провести сериализацию с помощью каких-нибудь стандартных механизмов (например в JSON)
8. И тоже посчитать время и прислать результат сравнения
9. Написать десериализацию/загрузку данных из строки (ini/csv-файла) в экземпляр любого класса
10. Замерить время на десериализацию
11. Общий результат прислать в чат с преподавателем в системе в таком виде:
	* Сериализуемый класс: class F { int i1, i2, i3, i4, i5;}
	* код сериализации-десериализации: ...
	* количество замеров: 1000 итераций
	* мой рефлекшен:
		* Время на сериализацию = 100 мс 
		* Время на десериализацию = 100 мс
	* стандартный механизм (NewtonsoftJson):
		* Время на сериализацию = 100 мс
		* Время на десериализацию = 100 мс

### Сериализуемый класс:

```cs
public class F
{
    public int i1, i2, i3, i4, i5;
    public F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
}
```

### Код сериализации-десериализации:
```cs
public static class MySerializer<T>
{
    // Сериализация
    public static string Serialize(T obj)
    {
        var sb = new StringBuilder();
        var myType = obj.GetType();
        var fields = myType.GetFields();

        foreach (var field in fields)
        {
            if (sb.Length > 0)
            {
                sb.Append(";");
            }                    
            sb.Append(field.Name);
            sb.Append("=");
            sb.Append(field.GetValue(obj).ToString());
                
        }
        return sb.ToString();
    }

    // Десериализация
    public static T DeSerialize(string s)
    {
        var myType = typeof(T);
        var instance = Activator.CreateInstance(myType);

        var strfieldValues = s.Split(';');
        foreach (var strfieldValue in strfieldValues)
        {
            var oneStrField = strfieldValue.Split('=');
            var field = myType.GetField(oneStrField[0]);
            field.SetValue(instance, Convert.ChangeType(oneStrField[1], field.FieldType));
        }

        return (T)instance;
    }
}
```
### Количество замеров: 1000000 итераций
Для замеров времени сеарилизации/десериализации я выбрал 1 000 000 иетераций
Код класса Program:

```cs
using hm3_serialization;
using Newtonsoft.Json;

F f = new F().Get();
int count = 1000000;
string objStr = "";

// Моя сериализация
var startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    objStr = MySerializer<F>.Serialize(f);
}

startTime.Stop();
Console.WriteLine($"My serialization time: {startTime.Elapsed}");
Console.WriteLine($"My searilizated string: {objStr}");

// Моя десериализация
startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    F deserializedF = MySerializer<F>.DeSerialize(objStr);
}
startTime.Stop();
Console.WriteLine($"My deserialization time: {startTime.Elapsed}");
Console.WriteLine();


// Newtonsoft.Json сериализация
string jsonStr = "";
startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    jsonStr = JsonConvert.SerializeObject(f);
}
startTime.Stop();
Console.WriteLine($"Newtonsoft.Json serialization time: {startTime.Elapsed}");


// Newtonsoft.Json десериализация
startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    F deserializedJsonF = JsonConvert.DeserializeObject<F>(jsonStr);
}
startTime.Stop();
Console.WriteLine($"Newtonsoft.Json deserialization time: {startTime.Elapsed}");
```
### Мой рефлекшен:
#### Время на сериализацию = 1.1005134 сек
#### Время на десериализацию = 1.4713107 сек

### Стандартный механизм (NewtonsoftJson):
#### Время на сериализацию = 1.1768401 сек
#### Время на десериализацию = 1.9117951 сек

### Результат работы программы:
<image src="images/result.png" alt="result">