# Code Convention Guides

This style guide is based on C# and Unity conventions. 

[[_TOC_]]


## Tổng quát

- Nhìn chung thì việc đặt tên vẫn theo chuẩn ngôn ngữ lập trình hướng đối tượng  C#.

| Object Name      | Notation   | Length | Plural | Prefix | Suffix | Abbreviation | Char Mask  | Underscores |
| ---------------- | ---------- | ------ | ------ | ------ | ------ | ------------ | ---------- | ----------- |
| Class name       | PascalCase | 128    | No     | No     | Yes    | No           | [A-z][0-9] | No          |
| Constructor name | PascalCase | 128    | No     | No     | Yes    | No           | [A-z][0-9] | No          |
| Method name      | PascalCase | 128    | Yes    | No     | No     | No           | [A-z][0-9] | No          |
| Method arguments | camelCase  | 128    | Yes    | No     | No     | Yes          | [A-z][0-9] | No          |
| Local variables  | camelCase  | 50     | Yes    | No     | No     | Yes          | [A-z][0-9] | No          |
| Constants name   | PascalCase | 50     | No     | No     | No     | No           | [A-z][0-9] | No          |
| Field name       | camelCase  | 50     | Yes    | No     | No     | Yes          | [A-z][0-9] | Yes         |
| Properties name  | PascalCase | 50     | Yes    | No     | No     | Yes          | [A-z][0-9] | No          |
| Delegate name    | PascalCase | 128    | No     | No     | Yes    | Yes          | [A-z]      | No          |
| Enum type name   | PascalCase | 128    | Yes    | No     | No     | No           | [A-z]      | No          |

### Namespaces

- Namespace toàn bộ phải theo quy tắc PascalCase, trừ 1 số trường hợp ngoại lệ như GUI,HUD...
- Namespace phải đặt theo đúng root mà script được đặt: Ví dụ: Assets.StickMan.Scripts.DownloadManager

**AVOID**:

```csharp
com.raywenderlich.fpsgame.hud.healthbar
```

**PREFER**:

```csharp
RayWenderlich.FPSGame.HUD.Healthbar
```

### Classes & Interfaces

Classes & interfaces phải được viết theo **PascalCase**. VD: `CucCutThanBiController`. 

### Methods

Methods phải được viết theo **PascalCase**. VD:  `DoSomething()`. 

### Fields

- Tất cả non-static fields thì viết dưới dạng **camelCase**. 
- Fields không được phép public ra bên ngoài class sử dụng.
- Muốn nhập từ editor thì phải đặt fieds private và thêm `[SerializeField]`.

For example:

```csharp
public class MyClass 
{
    [SerializeField]
    private int publicField;
    int packagePrivate;
    private int myPrivate;
    protected int myProtected;
}
```

**AVOID:**

```csharp
private int _myPrivateVariable
```

**PREFER:**

```csharp
private int myPrivateVariable
```

Static fields thì được viết theo **PascalCase**:

```csharp
public static int TheAnswer = 42;
```
### Properties

All properties được viết theo **PascalCase**. VD:

```csharp
public int PageNumber 
{
    get { return pageNumber; }
    private set;
}
```

*Hạn chế set thẳng dữ liệu vào fields bằng properties. Dùng public method để làm việc đó*.

> Setter sử dụng nhiều trong editor, chỉ nên hạn chế public setter.

Chỉ dùng properties để truy xuất dữ liệu của fields.

```csharp
public int PageNumber => pageNumber;
```

**AVOID:**

```csharp
myClass.PageNumber = 5;
```

**PREFER:**

```csharp
myClass.SetPageNumber(5);
```



### Parameters

Parameters phải được viết theo chuẩn **camelCase**.

**AVOID:**

```csharp
void DoSomething(Vector3 Location)
```

**PREFER:**

```csharp
void DoSomething(Vector3 location)
```

Cần tránh các giá trị ký tự đơn ngoại trừ các biến lặp tạm thời.Vd:

```csharp
for (int i = 0; i < 10; i++) 
{
    Debug.Log("index=" + i);
}
```

### Actions - Event

Actions viết theo chuẩn **PascalCase** và phải bắt đầu bằng On và theo quy tắc động từ. Project ưu tiên dùng **Unity Event**

```csharp
public UnityEvent OnValueChanged;
public event Action OnPickup;
```

### Arrays

Đặt tên mảng tương tự như các quy tắt ở trên, tuy nhiên tên mảng sẽ là danh từ số nhiều thay vì tiền tố hậu tố là list,array.

**AVOID:**

```csharp
private List<Gun> listGun;
public List<Gun> ListGun;
```

**PREFER:**

```csharp
private List<Gun> guns;
public List<Gun> Guns;
```

### Function

- 1 function nên là 1 động từ.

- Tất cả các function trả về bool đều phải bắt đầu bằng câu hỏi. VD: IsDead,CanFly,HasWeapon.

**AVOID:**

```csharp
private int PlayerState (){}; // Vô nghĩa. 
private int Dead (){}; // Đã chết? Sẽ chết???
private int Color (){}; // Không rõ ràng mục đích
```

**PREFER:**

```csharp
private int GetPlayerState(){};
private void IsDead(){}; // "Is" is a verb.
private int GetColorMain (){};
```

### Viết tắt - Từ nóng

Trong mã, các từ viết tắt nên được coi là từ. VD:

**AVOID:**

```csharp
XMLHTTPRequest
String URL
findPostByID
```

**PREFER:**

```csharp
XmlHttpRequest
String url
findPostById
```

## Khai báo - Định nghĩa

### Thứ tự khai báo

Để đạt được sự thống nhất và thói quen nhất quán trong coding style. Quy ước việc khái báo và triển khai code theo thứ tự sau đây:

1. Static varriables => private,protected, internal public...
2. Exposed variables => [SeriablizedField], private, protected, internal, public ...
3. Other variables => private protected internal public ...
4. Properties => { public getter; }
5. Properties => { public setter; }
6. Properties => private protected internal public { getter; setter; }
7. Constructors (if any)
8. Indexers (if any)
9. Unity lifecycle event => Awake, OnEable, Start, FixedUpdate, Update, LateUpdate, OnDisable, OnDestroy,...
10. Other methods => private, protected, internal, public,...

### Fields & Variables

Khai báo 1 đối tượng trên 1 dòng.

**AVOID:**

```csharp
string username, twitterHandle;
```

**PREFER:**

```csharp
string username;
string twitterHandle;
```

### Classes

Không được có 2 class trong 1 file code và không nên khai báo class lồng nhau nếu có sử dụng class con ở bên ngoài class cha.

### Interfaces

Tất cả Interface phải bắt đầu bằng **I**.

**AVOID:**

```csharp
Use
```

**PREFER:**

```csharp
IUse
```

## Code Format - Spacing

### Tổng quát

Thụt lề phải dùng phím `Tab` thay vì dùng `Space`.

### Khối code

Khoảng cách thụt lề là 4 dấu cách `Space`.

> Trong VisualStudio chọn **Tool** => **Options** => **Text Editor** => **C#** => Tabs
>
> Điều chỉnh Tab size và Indent size

**AVOID:**

```csharp
for (int i = 0; i < 10; i++) 
{
  Debug.Log("index=" + i);
}
```

**PREFER:**

```csharp
for (int i = 0; i < 10; i++) 
{
    Debug.Log("index=" + i);
}
```

### Xuống dòng

Thụt lề cho dòng bao trùm nên sử dụng **4 khoảng trắng** (không phải 8 mặc định):

**AVOID:**

```csharp
CoolUiWidget widget =
        someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
```

**PREFER:**

```csharp
CoolUiWidget widget =
    someIncrediblyLongExpression(that, reallyWouldNotFit, on, aSingle, line);
```

### Độ dài dòng code

- Các dòng không được dài hơn **100** ký tự.
- Nếu dòng code chưa câu lệnh quá dài liên tiếp cần xuống dòng theo tài liệu ở trên.


### Dấu ngoặc nhọn

```
Tất cả các dấu ngoặc nhọn đều có dòng riêng vì nó là một quy ước C#:
```

**AVOID:**

```csharp
class MyClass {
    void DoSomething() {
        if (someTest) {
          // ...
        } else {
          // ...
        }
    }
}
```

**PREFER:**

```csharp
class MyClass
{
    void DoSomething()
    {
        if (someTest)
        {
          // ...
        }
        else
        {
          // ...
        }
    }
}
```

Các câu lệnh điều kiện luôn bắt buộc phải đặt trong dấu ngoặc nhọn, không phân biệt số lượng dòng:

**AVOID:**

```csharp
if (someTest)
    doSomething();  

if (someTest) doSomethingElse();
```

**PREFER:**

```csharp
if (someTest) 
{
    DoSomething();
}  

if (someTest)
{
    DoSomethingElse();
}
```
### Switch -Case

Các câu lệnh switch đi kèm với trường hợp `default` theo mặc định. Nếu trường hợp `default` không bao giờ xảy ra, hãy loại bỏ nó.

**AVOID:**  

```csharp
switch (variable) 
{
    case 1:
        break;
    case 2:
        break;
    default:
        break;
}
```

**PREFER:**  

```csharp
switch (variable) 
{
    case 1:
        break;
    case 2:
        break;
}
```

### Commenting

- Comment giúp việc hiệu chỉnh và sửa lỗi 1 cách nhanh và chính xác hơn cho bản thân và người không phải là tác giả của đoạn code.
- Comment cho tất cả mọi thứ properties,fields,variable.
- Bắt buộc comment đối với các hàm public ra để gọi đến. Gồm 1 số thông tin sau:
  - Khái quát chức năng mà hàm sẽ thực hiện nếu chức năng phức tạp thì cần ghi lại thuật toán xử lí
  - Mô tả tham số truyền vào và các yêu cầu liên quan
  - Mô tả kết quả trả về của hàm nếu có giá trị trả về.

#### Comment Style

- Đặt comment trên một dòng riêng biệt, không phải ở cuối dòng mã.
- Bắt đầu comment bằng một chữ cái viết hoa.
- Kết thúc comment bằng một dấu chấm.
- Chèn một khoảng trắng giữa dấu phân cách  (//) và comment. VD: 

```
// Sample comment above a variable.
private int myInt = 5;
```

