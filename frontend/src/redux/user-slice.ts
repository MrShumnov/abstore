import { createSlice,current } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import ICartProduct from '../types/ICartProduct'
import IProduct from '../types/IProduct'
import IUser from '../types/IUser'

export interface UserState {
    user: IUser | null
}

const initialState: UserState = {
    user: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user') ?? "") : null
}

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        addToken: (state, action: PayloadAction<string>) => {
            if (!state.user)
                state.user = {} as IUser;
                
            state.user.token = action.payload;
        },

        addUserInfo: (state, action: PayloadAction<{id: number, username: string}>) => {
            if (!state.user)
                state.user = {} as IUser;

            state.user.id = action.payload.id;
            state.user.username = action.payload.username;
        },

        remove: (state) => {
            state.user = null;
        }
    }
})

// Action creators are generated for each case reducer function
export const { addToken, addUserInfo, remove } = userSlice.actions

export default userSlice.reducer