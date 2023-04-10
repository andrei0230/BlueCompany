# BlueCompany - QuestTracker

QuestTracker is an API that helps you manage quests and users. This README provides information about the various endpoints available in the API.

## Endpoints

### Quests endpoints

#### GET "api/Quest"
Gets all quests.

#### GET "api/Quest/{id}"
Gets the specified quest.

#### POST "api/Quest"
Creates a new quest and subtracts the value from the owner's tokens.\
Request body example:\
<img width="206" alt="image" src="https://user-images.githubusercontent.com/92015345/230868780-36fadae3-bb40-42c3-9438-b80ba047ec6e.png">

#### PUT "api/Quest/{id}"
Updates the specified quest's title and description.\
Request body example:\
<img width="233" alt="image" src="https://user-images.githubusercontent.com/92015345/230869301-daf26cd3-3040-4e5e-9598-bec1b262a7a8.png">

#### DELETE "api/Quest/{id}"
Deletes the specified quest.\

#### PUT "api/Quest/{id}/assign/{userId}"
Assigns a quest to a user.

#### PUT "api/Quest/complete/{id}"
Completes the specified quest and adds tokens to the assigned user.

### User endpoints

#### GET "api/User"
Gets all users.

#### GET "api/User/{id}"
Gets the specified user.

#### POST "api/User"
Creates a new user.\
Request body example:\
<img width="121" alt="image" src="https://user-images.githubusercontent.com/92015345/230870399-cdbf796b-62fe-46f5-8a56-56df4f1d9e48.png">

#### PUT "api/User/{id}"
Updates the specified user.\
Request body example:\
<img width="109" alt="image" src="https://user-images.githubusercontent.com/92015345/230870629-e98e53b9-1d10-4938-853f-205651e2befe.png">

#### DELETE "api/User/{id}"
Deletes the specified user.
