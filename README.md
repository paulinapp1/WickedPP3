# **Game Overview**

**Play as Elphaba**: The bold rebel fighting for justice.  
**Play as Glinda**: The charming star embracing fame and influence.

This game is set in the world of Oz, where the primary objective is to collect as many followers as possible. The game features two playable characters, Elphaba (the Wicked Witch of the West) and Glinda (the Good Witch of the North), with the difficulty of gameplay determined by the size of the map.

The game ends when the player encounters any obstacle, such as a wall or a tree.

Good luck!

---

# **How to Run Locally**

## **Prerequisites**

Before beginning, ensure that the following dependencies are installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) (or any code editor such as Visual Studio Code)
- [MongoDB](https://www.mongodb.com/try/download/community) (if used for the database)

---

## **Steps to Get Started**

### **1. Clone the Repository**

Begin by cloning the repository to your local machine using the following command:

```bash
git clone https://github.com/paulinapp1/WickedPP3.git

```
### **2. Install Dependencies**

Upon opening the solution, Visual Studio should automatically restore the required dependencies. If this does not happen, you can manually restore them by navigating to:
 -Tools > NuGet Package Manager > Restore NuGet Packages

### **3. Configure the Application**

In the appsettings.json file, update the MongoDB configuration to match your environment. Insert your MongoDB connection string and other related settings as shown below:

{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://your-mongo-db-url",
    "WickedGame": "WickedGame",
    "User": "User",
    "WickedScores": "WickedScores"
  }
}


## **4. Run the Application**

To launch the application, simply click Start (the green play button) in Visual Studio or press F5. The application will open in your default web browser, where you can begin playing the game.

If you are using MongoDB locally, ensure that your MongoDB instance is running and accessible to the application.

Additionally, you can modify the game’s difficulty by adjusting the map size or other settings within the GameInstance class.


