// Create a 4x4 room.
// Moving from room to room
// Most rooms are empty, except: 1) cavern entrance 2) fountain room with the fountain.
// go to fountain, activate and go back, then you win. 

Map map = new Map();
Renderer renderer = new Renderer();
Player player = new Player();

renderer.Render(map);
Console.WriteLine($"({player.CurrentLocation.Row}, {player.CurrentLocation.Column})");
while (true)
{
    string? input = Console.ReadLine();
    switch (input)
    {
        case "n":
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
            player.CurrentLocation.Row = player.CurrentLocation.Row - 1;
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
            break;
        case "s":
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
            player.CurrentLocation.Row = player.CurrentLocation.Row + 1;
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
            break;
        case "w":
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
            player.CurrentLocation.Column = player.CurrentLocation.Column - 1;
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
            break;
        case "e":
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
            player.CurrentLocation.Column = player.CurrentLocation.Column + 1;
            map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
            break;
    };

    renderer.Render(map);
    Console.WriteLine(map.Get(player.CurrentLocation));
    Console.WriteLine($"({player.CurrentLocation.Row}, {player.CurrentLocation.Column})");
    if (player.CurrentLocation.Row == 0 && player.CurrentLocation.Column == 0)
    {
        Console.Write("You see light in this room coming from outside the cavern. This is the entrance.");
    }

}

public class Player
{
    public Coordinate CurrentLocation { get; set; }

    public Player()
    {
        CurrentLocation = new Coordinate(0, 0);
    }

}

public class Fountain
{
    public Coordinate CurrentLocation { get; set; }

    public Fountain()
    {
        CurrentLocation = new Coordinate(0, 2);
    }
}

public class Map
{
    private RoomState[,] _room = new RoomState[4, 4];

    public RoomState Get(Coordinate coordinate)
    {
        return _room[coordinate.Row, coordinate.Column];
    }

    public void Set(Coordinate coordinate, RoomState newState)
    {
        _room[coordinate.Row, coordinate.Column] = newState;
    }
}

public class Renderer
{
    public void Render(Map map)
    {
        char[,] room = new char[4, 4];
        for (int row = 0; row < 4; row++)
            for (int column = 0; column < 4; column++)
                room[row, column] = ToChar(map.Get(new Coordinate(row, column)));

        Console.WriteLine($"┌───┬───┬───┬───┐");
        Console.WriteLine($"│ {room[0, 0]} │ {room[0, 1]} │ {room[0, 2]} │ {room[0, 3]} │");
        Console.WriteLine($"├───┼───┼───┼───┤");
        Console.WriteLine($"│ {room[1, 0]} │ {room[1, 1]} │ {room[1, 2]} │ {room[1, 3]} │");
        Console.WriteLine($"├───┼───┼───┼───┤");
        Console.WriteLine($"│ {room[2, 0]} │ {room[2, 1]} │ {room[2, 2]} │ {room[2, 3]} │");
        Console.WriteLine($"├───┼───┼───┼───┤");
        Console.WriteLine($"│ {room[3, 0]} │ {room[3, 1]} │ {room[3, 2]} │ {room[3, 3]} │");
        Console.WriteLine($"└───┴───┴───┴───┘");
    }

    private char ToChar(RoomState room)
    {
        return room switch
        {
            RoomState.Empty => ' ',
            RoomState.Player => 'X',
        };
    }
}

public class Coordinate
{
    public int Row;
    public int Column;

    public Coordinate(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }
}

public enum RoomState { Empty, Player }