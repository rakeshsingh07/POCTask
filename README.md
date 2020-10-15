# POCTask
Card arrangement and sorting
Popup System
FTU System
Highlight multiple UI items
User Inventory

Card arrangement and sorting:
Load information about cards to be loaded from card_data.json file.
Arrange cards horizontally.
Users should be able to select one or more cards & make a group out of it.
Ability to drag & drop a card from one group to another.
If the number of cards in a group is zero, delete that group.

Popup system:
Ability to show one popup on top of another. Should work like a stack. Most recent one should be on top.
Only one popup to be visible at a time.
All popups to have a back button on top
All popups have to have a header.
Each popup to have ability to accept anydata type & show in UI accordingly. Ex a json structure, List, int etc
Functionality to remove all popups at once. Ex: lets say we push 3 Popups one after another, we should be able to remove all popups in the list


Highlight multiple UI items:
This is in the context of tutorials. We need the ability to highlight 1 or more UI items as shown below. The image below shows just one card being highlighted, we need functionality to highlight multiple items.
The Canvas & highlighting system to be on separate screens (Canvas), Item(s) to be highlighted must be dynamic.

Just like K highlighted above, we should be able to highlight 2 (Hearts) as well.


User Inventory:
Create a PlayerData class that holds user inventory such as purchased items
Load & save PlayerData in PlayerPrefs
Create a StoreData class that has ‘n’ no of items loaded from a json file store_data.json.
Show the items loaded from store_data.json in store UI.
Once a user purchases, add it to user inventory.
The store item once purchased, should not be shown again.

