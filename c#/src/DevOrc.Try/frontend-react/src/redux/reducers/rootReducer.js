import { combineReducers } from 'redux';
import * as formTodo from './todoReducer';

export const initialState = {
    todos: formTodo.initialState
};

export const rootReducer = combineReducers({
    todos: formTodo.reducer

});