import isEmpty from '../validation/is-empty';
import {SET_CURRENT_USER} from '../actions/types';




//reducer defines how the application state should change in response to an action


//initial state
const initialState = {
    isAuthenticated: false,
    user: {}
}
//reducer is a pure function that takes state and action as parameter
//it responds to the action type triggered by the action creator
export default function(state = initialState, action){
    switch(action.type){
        case SET_CURRENT_USER: //uses this to identify the action that took place
            return{
                ...state, //return current state
                isAuthenticated: !isEmpty(action.payload), //check if user paayload is empty or not
                user: action.payload   //the payload is a n object with the decoded user from authaction --setcurrentuser
            }
        default:
            return state; 
    }
}