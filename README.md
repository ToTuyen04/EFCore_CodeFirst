## Demo: Áp dụng Code First trong Entity Framework Core

Dưới đây là hướng dẫn demo cách sử dụng Code First với Entity Framework Core trong project này.

### 1. Cấu trúc project
Project gồm các thư mục chính:
- `Controllers/`: Chứa các controller cho API.
- `Db/Models/`: Chứa các class entity (Player, InstrumentType, PlayerInstrument).
- `Db/Migrations/`: Chứa các migration được tạo ra từ lệnh EF Core.
- `Service/`: Chứa các service xử lý logic nghiệp vụ.
- `Views/`: Chứa các view Razor.
- `Dto/`: Chứa các lớp truyền dữ liệu (request/response).

### 2. Các bước Code First
#### a. Định nghĩa các Entity
Ví dụ: `Player.cs`
```csharp
public class Player
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<PlayerInstrument> PlayerInstruments { get; set; }
}
```

#### b. Tạo DbContext
Ví dụ: `CodeFirstDemoContext.cs`
```csharp
public class CodeFirstDemoContext : DbContext
{
	public DbSet<Player> Players { get; set; }
	public DbSet<InstrumentType> InstrumentTypes { get; set; }
	public DbSet<PlayerInstrument> PlayerInstruments { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("<your_connection_string>");
	}
}
```

#### c. Tạo Migration và cập nhật database
Chạy các lệnh sau trong terminal:
```powershell
# Tạo Migration
dotnet ef migrations add InitialMigration --project EFCore_CodeFirst
# Cập nhật Database:
dotnet ef database update --project EFCore_CodeFirst
```

#### d. Seed dữ liệu mẫu (nếu cần)
Xem file `DbSeeder.cs` để seed dữ liệu mẫu vào database.

### 3. Chạy ứng dụng
Chạy lệnh sau để khởi động ứng dụng:
```powershell
dotnet run --project EFCore_CodeFirst/EFCore_CodeFirst.csproj
```
Truy cập các endpoint API hoặc giao diện Razor để kiểm tra dữ liệu.

### 4. Tham khảo thêm
- [Tài liệu Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

