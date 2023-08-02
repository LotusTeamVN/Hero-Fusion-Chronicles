# Hệ thống Input của Unity

Input cho phép người dùng điều khiển ứng dụng thông qua sử dụng thiết bị, chạm, cử chỉ,...

- **Unity hỗ trợ input từ các loại thiết bị:**  

  - Bàn phím và chuột 

  - Joystick 

  - Controller 

  - Màn hình cảm ứng 

  - Cảm biến di chuyển của di động 

  - Thiết bị VR và AR 

- **Unity hỗ trợ input thông qua 2 hệ thống riêng biệt:**  

  - Input Manager là 1 phần cơ bản của nền tảng Unity, được sử dụng mặc định 

  - Input System là 1 package được cài đặt từ Package Manager để có thể sử dụng. Yêu cầu .NET 4 và không hoạt động ở project có cài đặt phiên bản cũ hơn .NET 3.5 

## I. Về Input Sytem

Input System package được kế thừa từ 1 hệ thống có thể sử dụng bất cứ thiết bị đầu vào nào mà Unity hỗ trợ để điểu khiển/kiểm soát nội dụng bên trong Unity. 

Hệ thống này được cho là mạnh mẽ hơn, linh hoạt hơn và dễ dàng cấu hình cũng như thay thế so với hệ thống Input sẵn có của Unity là Input Manager(UnityEngine.Input class). 

### 1. Cài đặt Input System

Để sử dụng Input System cần phải cài đặt và kích hoạt Input System package cho Unity Project

>**Chú ý:**  Input System yêu cầu Unity 2009.1 trở lên và môi trường chạy .NET 4. Không làm việc với .NET 3.5 hoặc cũ hơn

**Cài đặt Package:** 

Để cài đặt Input System mở Unity Package Manager(menu: **Window** > **Package Manager**). Chọn **Input System** package từ danh sách và ấn Install

<img src="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/images/InputSystemPackage.png" />

Sau khi cài đặt cần phải thay đổi từ Input mặc định là Input Manager có sẵn trong Unity sang Backends mới cho Input System nên sẽ có thông báo yêu cầu khởi động lại **Editor**

<img src="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/images/EditorRestartWarning.png" />

Chọn **Yes** > Khởi động lại Unity

### 2. Hướng dẫn sử dụng: 
Có 2 cách để lấy được Input thông qua Input System là:
  - Nhận Input trực tiếp từ thiết bị đầu vào(Input Device)
  - Nhận Input gián tiếp thông qua Input Action

**Nhận Input trực tiếp từ thiết bị đầu vào**

Là cách nhanh nhất để bắt đầu từ Script đọc trạng thái hiện tại trực tiếp từ Thiết bị đầu vào. Ví dụ, đoạn code sau đây lấy ra GamePad mà Player vừa sử dụng và đọc ra trạng thái hiện tại: 

    using UnityEngine;
    using UnityEngine.InputSystem;

    public class MyPlayerScript : MonoBehaviour
    {
      void FixedUpdate()
      {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            // 'Use' code here
        }

        Vector2 move = gamepad.leftStick.ReadValue();
        // 'Move' code here
      }
    }

Tương tự có thể áp dụng cho các Thiết bị khác(Ví dụ: bàn phím và chuột sử dụng Keyboard.current và Mouse.current)

**Nhận Input thông qua Input Action**

Các bước:
  1. Thêm PlayerInput Component
  2. Tạo Input Actions
  3. Cài đặt Action Responses

**Bước 1: Thêm PlayerInput Component**

Lấy Input trực tiếp từ thiết bị đầu vào thì nhanh chóng, tiện lợi nhưng cũng yêu cầu liên kết riêng cho mỗi loại thiết bị. Điều này cũng khó khăn hơn cho việc thay đổi sau này khi mà các thiết bị điều khiển muốn trigger 1 sự kiện đặc biệt trong game. 

Thay vào đó, ta có thể sử dụng Actions như là 1 đối tượng trung gian giữa Thiết bị và những phản hồi trong game mà chúng Trigger. Cách dễ dàng nhất để làm việc này là sử dụng PlayerInput component. 

<img src="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/images/AddPlayerInput.png" />


**Bước 2: Tạo Input Action**

Mỗi PlayerInput component đại diện cho 1 player trong game. Để nhận Input thì component phải được kết nối với Input Action. 
Cách nhanh nhất để tạo là click vào nút **Create Action...** trong cửa sổ inspector. Tạo ra 1 Asset chứa các cài đặt lúc đầu của Input Action Map, Input Action và Input Binding

<img src="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/images/PlayerInputCreateActions.png" />

Sau khi bấm **Create Action** hiện lên bảng(phía dưới) để cài đặt, chỉnh sửa Input từ các thiết bị đầu vào cho phù hợp với dự án. 
Để hiểu rõ hơn cách sử dụng cửa sổ này có thể tìm hiểu ở [đây](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionAssets.html#editing-input-action-assets)

<img src="https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/images/MyGameActions.png" />

**Bước 3: Cài đặt Action Response**
Mỗi Component Player Input 
