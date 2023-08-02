Naming Convention
===========================

Để phát triển phần mềm tốt hơn thì việc chuẩn hóa tên khá quan trọng.

**Mục Lục**

[[_TOC_]]

## Quy ước chung

- Tất cả mọi tài nguyên đặt tên đều không được phép chứa khoảng trắng.
- Trừ 1 số trường hợp đặc biệt của artist thì các trường hợp còn lại không dùng gạch dưới,tuân thủ theo quy tắc `PascalCase`.

### Folders

`PascalCase`

Tên folder nên ngắn gọn nhất có thể, ưu tiên một hoặc hai từ. Nếu một tên thư mục quá dài, có lẽ nên chia nó thành các thư mục con.

Cố gắng 1 thư mục chỉ đại diện cho 1 loại nhất định. VD: Sửu dụng `Textures/Trees`, `Models/Trees` thay vì `Trees/Textures`, `Trees/Models`. 

Trong trường hợp có những sự vật giống nhau về tính chất nhưng khác nhau về môi trường thì sử dụng : `Trees/Jungle`, `Trees/City` thay vì `Jungle/Trees`, `City/Trees`. Điều này sẽ khiến việc tìm kiếm và so sánh sự khác nhau giữa 2 sự vật có cùng tính chất nhưng khác môi trường một cách dễ dàng hơn rất nhiều.

### Các tài nguyên 3D

Sử dụng `tree_small` thay vì `small_tree`. Mặc dù small_tree nghe có vẻ đúng với tiếng anh. Tuy nhiên việc gom nhóm theo alphaB sẽ rất bất tiện khi tìm kiếm `tree` các kích thước khác nhau.

`camelCase` thì cũng sẽ được sử dụng khi cần thiết. Sử dụng `weapon_miniGun` thay thế `weapon_gun_mini`. Hãy tránh điều này nếu có thể bởi tiếp theo có thể bạn sẽ muốn có nhiều loại miniGun.

Sử dụng các hậu tố miêu tả kĩ thay vì dùng các số thứ tự bừa bãi: `vehicle_truck_damaged` not `vehicle_truck_01`.

### Các Object quan trọng

`_snake_case`

```
Sử dụng dấu gạch dưới ở đầu để làm nổi bật các trường hợp đối tượng không cụ thể cho scene hiện tại.
```

## Cấu trúc thư mục

```
Root
+---Assets
+---Build
\---Tools           # Programs to aid development: compilers, asset managers etc.
```

### Assets

```
Assets
+---ProjectName
	+---Art
		+---Common	# Dùng chung
 		+---Unit    # Dùng riêng
	+---Audio
   		+---Music
   		\---Sound       
	+---Code
	    +---Scripts     # C# scripts
	    \---Shaders     # Shader files and shader graphs
	+---Docs            # Wiki, concept art, marketing material
	+---Level           # Tất cả các scene release đặt trong đây
    +---Prefabs
    +---Scenes          # Scene test hoặc chưa release đặt vào đây
    \---UI
	\---Resources       # Configuration files, localization text and other user files.
```

### Scripts

Use namespaces that match your directory structure.

A Framework directory is great for having code that can be reused across projects.

The Scripts folder varies depending on the project, however, `Environment`, `Framework`, `Tools` and `UI` should be consistent  across projects.

```
Scripts
+---Environment
+---Framework
+---NPC
+---Player
+---Tools
\---UI
```

## Quy tắc đặt tên asset

Một asset phải được đặt tên theo quy tắt `Prefix_BaseAssetName_Variant_Suffix`. Sử dụng các tiền tố hậu tố theo mô tả dưới đây.

### Most Common

| Asset Type         | Prefix | Suffix    | Notes                                                        |
| ------------------ | ------ | --------- | ------------------------------------------------------------ |
| Level / Scene      | *      |           | Should be in a folder called Levels.`Levels/A4_C17_Parking_Garage.unity` |
| Level (Persistent) |        | _P        |                                                              |
| Level (Audio)      |        | _Audio    |                                                              |
| Level (Lighting)   |        | _Lighting |                                                              |
| Level (Geometry)   |        | _Geo      |                                                              |
| Level (Gameplay)   |        | _Gameplay |                                                              |
| Prefab             |        |           |                                                              |
| Material           | M_     |           |                                                              |
| Static Mesh        | SM_    |           |                                                              |
| Skeletal Mesh      | SK_    |           |                                                              |
| Texture            | T_     | _?        | See [Textures](https://github.com/justinwasilenko/Unity-Style-Guide#anc-textures) |
| Particle System    | PS_    |           |                                                              |
| Terrain			 |        | _Terrain  | {+ NEW +}													 |
| NavMesh			 |		  | _NavMesh  |	{+ NEW +}													 |

### Models (FBX Files)

`PascalCase`

| Asset Type    | Prefix | Suffix | Notes |
| ------------- | ------ | ------ | ----- |
| Characters    | CH_    |        |       |
| Vehicles      | VH_    |        |       |
| Weapons       | WP_    |        |       |
| Static Mesh   | SM_    |        |       |
| Skeletal Mesh | SK_    |        |       |
| Skeleton      | SKEL_  |        |       |
| Rig           | RIG_   |        |       |

### Animations

| Asset Type           | Prefix | Suffix | Notes |
| -------------------- | ------ | ------ | ----- |
| Animation Clip       | A_     |        |       |
| Animation Controller | AC_    |        |       |
| Avatar Mask          | AM_    |        |       |
| Morph Target         | MT_    |        |       |

### Artificial Intelligence

| Asset Type        | Prefix       | Suffix  | Notes |
| ----------------- | ------------ | ------- | ----- |
| AI Controller     | AIC_         |         |       |
| Behavior Tree     | BT_          |         |       |
| Blackboard        | BB_          |         |       |
| Decorator         | BTDecorator_ |         |       |
| Service           | BTService_   |         |       |
| Task              | BTTask_      |         |       |
| Environment Query | EQS_         |         |       |
| EnvQueryContext   | EQS_         | Context |       |

### Prefabs

| Asset Type        | Prefix | Suffix | Notes                                |
| ----------------- | ------ | ------ | ------------------------------------ |
| Prefab            |        |        |                                      |
| Prefab Instance   | I      |        |                                      |
| Scriptable Object |        |        | Assigned "Blueprint" label in Editor |

### Materials

| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Material          | M_     |        |       |
| Material Instance | MI_    |        |       |
| Physical Material | PM_    |        |       |

### Textures

| Asset Type                          | Prefix | Suffix | Notes |
| ----------------------------------- | ------ | ------ | ----- |
| Texture                             | T_     |        |       |
| Texture (Diffuse/Albedo/Base Color) | T_     | _D     |       |
| Texture (Normal)                    | T_     | _N     |       |
| Texture (Roughness)                 | T_     | _R     |       |
| Texture (Alpha/Opacity)             | T_     | _A     |       |
| Texture (Ambient Occlusion)         | T_     | _AO    |       |
| Texture (Bump)                      | T_     | _B     |       |
| Texture (Emissive)                  | T_     | _E     |       |
| Texture (Mask)                      | T_     | _M     |       |
| Texture (Specular)                  | T_     | _S     |       |
| Texture (Packed)                    | T_     | _*     |       |
| Texture Cube                        | TC_    |        |       |
| Media Texture                       | MT_    |        |       |
| Render Target                       | RT_    |        |       |
| Cube Render Target                  | RTC_   |        |       |
| Texture Light Profile               | TLP_   |        |       |

### Physics

| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Physical Material | PM_    |        |       |

### Audio

| Asset Type     | Prefix | Suffix | Notes                                                        |
| -------------- | ------ | ------ | ------------------------------------------------------------ |
| Audio Clip     | A_     |        |                                                              |
| Audio Mixer    | MIX_   |        |                                                              |
| Dialogue Voice | DV_    |        |                                                              |
| Audio Class    |        |        | No prefix/suffix. Should be put in a folder called AudioClasses |

### User Interface(UI)

| Asset Type       | Prefix | Suffix | Notes |
| ---------------- | ------ | ------ | ----- |
| Font             | Font_  |        |       |
| Texture (Sprite) | T_     | _GUI   |       |

### Effects

| Asset Type      | Prefix | Suffix | Notes |
| --------------- | ------ | ------ | ----- |
| Particle System | PS_    |        |       |

---
