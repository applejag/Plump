# 1. Summary
Small project of my own on making a card game in unity.
The idea is to make it multiplayer.

# 2. Rules
Plump is played with a normal 52 playing cards deck.
Can be played with 2 or more players but is recommended to be played with 3 to 5 players.

Rules source: own local learning from family and friends

## 2.1 Rounds
Each round you play different number of ticks.
First round you play 10, next you play 9, then 8, then 7, etc. until you reach 1, then it goes up again to 2, then 3, then 4, etc., up to 10 ticks.<br/>
But when you reach the rounds with 1 tick you play as many rounds as there are players in the game.

Example of protocol:

| Ticks | Lisa | Stephen | Toby | Rachel |
| :---- | ---: | ------: | ---: | -----: |
| **10**|      |         |      |        |
| **9** |      |         |      |        |
| **8** |      |         |      |        |
| ...   |      |         |      |        |
| **3** |      |         |      |        |
| **2** |      |         |      |        |
| **1** |      |         |      |        |
| **1** |      |         |      |        |
| **1** |      |         |      |        |
| **1** |      |         |      |        |
| **2** |      |         |      |        |
| **3** |      |         |      |        |
| ...   |      |         |      |        |
| **8** |      |         |      |        |
| **9** |      |         |      |        |
| **10**|      |         |      |        |


After that the game is over and you calculate each players score.

**Note:** Because of the finite amount of cards you have to remove some rounds depending on the amount of players. For example the first and last round cannot be played with 6 or more players because it needs 10 cards per player and a normal deck only holds 52 cards.

### 2.1.1 Start of round
Each round starts with dealing out all cards. One of the players is assigned as a dealer (preferably a new player each round) and deals out as many cards as there are ticks that round to each player, including themselves.<br/>
The dealer starts with shuffling the deck and then dealing out one card at a time to each player clockwise.

When each player got their cards they are allowed to look at them and estimate how many ticks they will win this round. This amount is written down on the protocol.

The total amount of ticks said can not be the same as the number of ticks to be played.<br>
This means that the last player cannot say a tick that would sum everyones ticks to the number of ticks to be played that round.

Example of protocol for round 1 with 10 ticks:

| Ticks | Lisa | Stephen | Toby | Rachel |
| ----- | ---: | ------: | ---: | -----: |
| **10**| 1    | 3       | 2    | 0      |

In the above example if Lisa were to start saying her number of ticks, then Ratchel would not be able to say 4 because
<ul>Lisa's 1 + Stephen's 3 + Toby's 2 = 6 ticks</ul>
If Ratchel would've picked 4 then the total amount of ticks would sum up to 10, which is not allowed.

### 2.1.2 During round
Each round you play as for as many ticks as cards have been given out.
For example on the first round you play for 10 ticks.

#### 2.1.2.1 Tick
Each tick you play one lap around the table, where every player must lay out one card to the tick.

The first player lays out their starting card, which could be anything from their hand.<br/>
The next player must lay a card with the same suit if able, otherwise they lay out anything they wish from their hand.<br/>
After the last player has layed out their card the tick is given to the player who layed out the highest card with the same suit as the starting card.

The tick is ment to be hold for counting score, so keep track of your tick if you gain one.

**Note:** Every tick must be won by a player, if noone plays over the starting card then the first player wins the tick.

### 2.1.3 End of round
After all ticks have been played, *i.e.* everyones hands are empty, the score is calculated for that round.

### 2.1.4 One tick rounds
In the one tick round, every player only gets one card each, as the **2.1.1** say.<br/>
But instead of each player looking at their own cards they quickly put the card to their forehead so only others can see the card.

Estimating if you would win the one and only tick is a bit tricky when you _[as a player]_ can't see your own card, but you can guess by looking at everyone else's cards.

### 2.1.5 Score
When the round is over eveyone counts their ticks gained during that round.<br/>
If you _[as a player]_ did not got the same amount of ticks as you said you were in the beginning of the round, you get "plumped", which means that you dont get any points for that round.<br/>
But if you _[as a player]_ would get the same amount your score is calculated by just adding a 1 in front of the number of ticks you estimated.

Example continuing from the previous table in **2.1.1**:

* Lisa gets 2 ticks
* Stephen gets 3 ticks
* Toby gets 5 ticks
* Rachel gets 0 ticks

The protocol would then look something like this:

| Ticks | Lisa | Stephen | Toby | Rachel |
| ----- | ---: | ------: | ---: | -----: |
| **10**| 🌑   | 13      | 🌑   | 10     |

**Note:** This means that if you _[as a player]_ estimate you will win 10 ticks and actually do win those 10, your score becomes 110!

## 2.2 Final score
The final score is just a sum of all rounds score, not counting the *plumps*.<br/>
Example continueing from previous table in **2.1.5**:

| Ticks | Lisa | Stephen | Toby | Rachel |
| :---- | ---: | ------: | ---: | -----: |
| **10**    | 🌑   | 10      | 🌑    | 🌑    |
| **9**     | 🌑   | 🌑      | 14   | 🌑     |
| **8**     | 🌑   | 🌑      | 🌑   | 🌑     |
| **7**     | 12   | 🌑      | 🌑   | 11     |
| **6**     | 🌑   | 🌑      | 🌑   | 10     |
| **5**     | 11   | 🌑      | 🌑   | 🌑     |
| **4**     | 🌑   | 🌑      | 11   | 🌑     |
| **3**     | 🌑   | 🌑      | 🌑   | 11     |
| **2**     | 11   | 🌑      | 10   | 🌑     |
| **1**     | 🌑   | 🌑      | 10   | 🌑     |
| **1**     | 10   | 10      | 🌑   | 10     |
| **1**     | 10   | 10      | 10   | 🌑     |
| **1**     | 🌑   | 10      | 10   | 10     |
| **2**     | 10   | 11      | 🌑   | 🌑     |
| **3**     | 🌑   | 10      | 🌑   | 🌑     |
| **4**     | 🌑   | 🌑      | 12   | 🌑     |
| **5**     | 🌑   | 🌑      | 🌑   | 🌑     |
| **6**     | 🌑   | 🌑      | 11   | 🌑     |
| **7**     | 🌑   | 🌑      | 🌑   | 🌑     |
| **8**     | 🌑   | 🌑      | 🌑   | 🌑     |
| **9**     | 14   | 🌑      | 🌑   | 🌑     |
| **10**    | 🌑   | 🌑      | 🌑   | 🌑     |
| **Total** | **78**   | **61**      | **88**   | **52**     |

As you can see in this case **Toby** won.
