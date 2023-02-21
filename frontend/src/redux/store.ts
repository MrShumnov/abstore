import { configureStore } from '@reduxjs/toolkit'
import cartReducer from './cart-slice'
import userReducer from './user-slice'

const cartMiddleware = (store: any) => (next: any) => (action: any) => {
    const result = next(action);
    if ( action.type?.startsWith('cart/') ) {
        const cartState = store.getState().cart;
        localStorage.setItem('cart', JSON.stringify(cartState.items))
    }
    return result;
};

const userMiddleware = (store: any) => (next: any) => (action: any) => {
  const result = next(action);
  if ( action.type?.startsWith('user/') ) {
      const userState = store.getState().user;
      localStorage.setItem('user', JSON.stringify(userState.user))
  }
  return result;
};

export const store = configureStore({
  reducer: {
    cart: cartReducer,
    user: userReducer
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(cartMiddleware).concat(userMiddleware)
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch

