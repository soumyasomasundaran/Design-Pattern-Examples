using System;
using System.Collections.Generic;

// Abstract product
abstract class Weapon
{
    public abstract void Attack();
}

// Concrete products
class Sword : Weapon
{
    public override void Attack()
    {
        Console.WriteLine("Attacking with a sword!");
    }
}

class Bow : Weapon
{
    public override void Attack()
    {
        Console.WriteLine("Shooting arrows with a bow!");
    }
}

// Abstract factory
abstract class WeaponFactory
{
    public abstract Weapon CreateWeapon();
}

// Concrete factories
class SwordFactory : WeaponFactory
{
    public override Weapon CreateWeapon()
    {
        return new Sword();
    }
}

class BowFactory : WeaponFactory
{
    public override Weapon CreateWeapon()
    {
        return new Bow();
    }
}

// Singleton
class GameManager
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    private GameManager()
    {
        // Initialize the game manager
    }

    public void StartGame()
    {
        Console.WriteLine("Game started!");
    }
}

// Builder
class Character
{
    public Weapon Weapon { get; private set; }

    public void EquipWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    public void Attack()
    {
        if (Weapon != null)
        {
            Weapon.Attack();
        }
        else
        {
            Console.WriteLine("No weapon equipped!");
        }
    }
}

// Prototype
abstract class EnemyPrototype
{
    public abstract EnemyPrototype Clone();
    public abstract void Attack();
}

class Goblin : EnemyPrototype
{
    public override EnemyPrototype Clone()
    {
        return (EnemyPrototype)this.MemberwiseClone();
    }

    public override void Attack()
    {
        Console.WriteLine("Goblin attacks!");
    }
}

// Factory method
abstract class EnemyFactory
{
    public abstract EnemyPrototype CreateEnemy();
}

class GoblinFactory : EnemyFactory
{
    public override EnemyPrototype CreateEnemy()
    {
        return new Goblin();
    }
}

// Client
class Game
{
    private Character character;
    private List<EnemyPrototype> enemies;

    public Game(Character character)
    {
        this.character = character;
        enemies = new List<EnemyPrototype>();
    }

    public void AddEnemy(EnemyPrototype enemy)
    {
        enemies.Add(enemy);
    }

    public void StartGame()
    {
        Console.WriteLine("\n=== Game Start ===");
        Console.WriteLine("Your character attacks with:");
        character.Attack();

        Console.WriteLine("\nEnemies attack:");
        foreach (var enemy in enemies)
        {
            enemy.Attack();
        }
        Console.WriteLine("=== Game Over ===");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Game!");

        // Abstract factory pattern
        Console.WriteLine("\nSelect your weapon type:");
        Console.WriteLine("1. Sword");
        Console.WriteLine("2. Bow");
        Console.Write("Enter your choice: ");
        string weaponChoice = Console.ReadLine();

        WeaponFactory weaponFactory;
        switch (weaponChoice)
        {
            case "1":
                weaponFactory = new SwordFactory();
                break;
            case "2":
                weaponFactory = new BowFactory();
                break;
            default:
                Console.WriteLine("Invalid choice. Using default weapon (Sword).");
                weaponFactory = new SwordFactory();
                break;
        }

        Weapon weapon = weaponFactory.CreateWeapon();

        // Singleton pattern
        GameManager gameManager = GameManager.Instance;
        gameManager.StartGame();

        // Builder pattern
        Character character = new Character();
        character.EquipWeapon(weapon);

        // Prototype pattern
        Console.WriteLine("\nCreate an enemy (Goblin) by cloning:");
        Console.WriteLine("1. Clone");
        Console.WriteLine("2. Create New");
        Console.Write("Enter your choice: ");
        string enemyChoice = Console.ReadLine();

        EnemyPrototype goblin;
        switch (enemyChoice)
        {
            case "1":
                goblin = new Goblin();
                break;
            case "2":
                goblin = new Goblin().Clone();
                break;
            default:
                Console.WriteLine("Invalid choice. Creating a new enemy (Goblin).");
                goblin = new Goblin();
                break;
        }

        // Factory method pattern
        EnemyFactory enemyFactory = new GoblinFactory();
        EnemyPrototype enemy = enemyFactory.CreateEnemy();

        // Game
        Game game = new Game(character);
        game.AddEnemy(enemy);
        game.StartGame();

        Console.WriteLine("\nThanks for playing the Game!");
        Console.ReadLine();
    }
}
