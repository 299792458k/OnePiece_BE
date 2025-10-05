# Treasure Hunting API

API để giải bài toán tìm kho báu với chi phí nhiên liệu tối thiểu.

## Cấu trúc dự án

```
TreasureHunting/
├── Controllers/          # API Controllers
│   └── MapController.cs  # Endpoints cho treasure map
├── BL/                   # Business Logic Layer
│   ├── IBL/
│   │   └── IBLMap.cs     # Interface cho BL
│   └── BLMap.cs          # Business logic implementation
├── DL/                   # Data Access Layer
│   ├── IDL/
│   │   └── IDLMap.cs     # Interface cho DL
│   ├── DBContext/
│   │   └── AppDbContext.cs
│   └── DLMap.cs          # Data access implementation
├── Model/                # Data Models
│   └── TreasureMap.cs
├── Utility/              # Helper classes & DI setup
├── Logs/                 # Log files (tạo tự động)
├── nlog.config           # NLog configuration
└── appsettings.json      # App settings
```

### Chạy ứng dụng

1. Clone repository
2. Di chuyển đến thư mục source code
3. Restore packages:
```bash
dotnet restore
```
4. Chạy ứng dụng:
```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `http://localhost:5000` (hoặc port được chỉ định)

## Database

- **Provider**: SQLite
- **File**: `treasurehunting.db` (tạo tự động)
- **Tables**:
  - `TreasureMaps` - Lưu trữ thông tin treasure maps

Database được tạo tự động khi khởi động ứng dụng lần đầu.

## API Documentation

Swagger UI có sẵn khi chạy ở môi trường Development:
- URL: `http://localhost:5000/swagger`

## CORS

CORS được cấu hình cho phép tất cả origins, methods và headers (AllowAll policy).

