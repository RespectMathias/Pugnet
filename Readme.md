# Pugnet

**Pugnet** is a modern Pug view engine for ASP.NET Core, forked from [Pugzor](https://github.com/DavidPaquette/Pugzor). It is updated for .NET 8 and using Jering's `Javascript.NodeJS`.

---

Learn more about Pug at [pugjs.org](https://pugjs.org/api/getting-started.html).

---

## 🔧 Installation

```powershell
Install-Package Pugnet
```

*Note: Package availability coming soon.*

---

## 🔨 Usage

In your `Program.cs`:

```csharp
services.AddControllersWithViews()
        .AddPugnet();
```

Place your `.pug` files in the `Views` directory.

### Accessing Model Properties

**Model:**

```csharp
public class Model
{
    public string FirstName { get; set; } = "John";
}
```

**Pug Template:**

```pug
p= FirstName
```

---

## 📄 License

Apache 2.0 License. See [LICENSE](LICENSE).

---

<div align="center">
  <strong>Contributions and issues are welcome!</strong>
</div>
