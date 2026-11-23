# DummyOrderApp

Incident Management demo için oluşturulmuş örnek .NET Web API projesi.

## Kasıtlı Bug

`OrderService.ProcessOrder()` içinde `customerName` parametresi `null` geldiğinde
`NullReferenceException` fırlatılır. Bu hata Prometheus metrics üzerinden Grafana'ya yansır
ve Incident Management sistemi tarafından otomatik olarak bir PR ile düzeltilir.

## Çalıştırma

```bash
dotnet run --project src/DummyOrderApp.Api
```

## Endpoints

- `POST /api/orders/process` → Normal sipariş işleme
- `POST /api/orders/trigger-bug` → `null` customerName göndererek bug'ı tetikler
- `GET /metrics` → Prometheus metrics
