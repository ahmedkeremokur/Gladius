This is an unfinished 2D RPG Game. I'm still working on slowly but take a look:

![gladiusss](https://github.com/user-attachments/assets/92a751b7-bc5c-4a4f-af21-a6bc1381a9cd)
![gladiusss2](https://github.com/user-attachments/assets/fc18a25b-584e-477c-9645-89cde8781851)
![gladiusss3](https://github.com/user-attachments/assets/dee2ae2b-0215-43ac-935e-5b527c1cd9b8)
![gladius4](https://github.com/user-attachments/assets/40e1dd32-1f75-4090-bf6c-cf92660167ce)



I've devised calculations based on certain stats of the character and the opponent. The difference between the character's dexterity and the opponent's agility determines the likelihood of landing a critical hit, while the difference between the character's charisma and the opponent's wisdom sets the value of 'x' in the function that dictates the probability of a double strike.

I’ve developed three distinct yet similar functions, all dependent on the value of x, and plotted them on a graph for analysis. The graph is drawn in two different ranges: from 0 to 105 and from 0 to 50, allowing for a more detailed examination. The green line corresponds to a value of 1, indicating a certain outcome, while the purple line represents a value of 0.5, signifying a 50% probability. Among these functions, I chose the one defined as f(x) = 1 - e^(-x/50).

![allgraph](https://github.com/user-attachments/assets/688e83f2-13e5-45c5-84c5-9ae408a82b0f)
