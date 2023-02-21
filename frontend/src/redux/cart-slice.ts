import { createSlice,current } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import ICartProduct from '../types/ICartProduct'
import IProduct from '../types/IProduct'

export interface CartState {
    items: ICartProduct[]
}

const initialState: CartState = {
    items: JSON.parse(localStorage.getItem('cart') ?? "[]") as ICartProduct[]
}

export const cartSlice = createSlice({
    name: 'cart',
    initialState,
    reducers: {
        addProduct: (state, action: PayloadAction<IProduct>) => {
            let index = 0
            if (state.items.length > 0)
                index = Math.max(...state.items.map((val: any) => val.index)) + 1;

            let cartProduct = {
                index: index,
                id: action.payload.id, 
                symbol: action.payload.symbol, 
                price: action.payload.price, 
                sale: action.payload.sale
            } as ICartProduct;

            state.items.push(cartProduct);
        },

        moveProduct: (state, action: PayloadAction<{itemIndex: number, shift: number}>) => {
            let index = state.items.findIndex((val: any) => val.index === action.payload.itemIndex);

            if (index !== -1 && index + action.payload.shift < state.items.length 
                                && index + action.payload.shift >= 0) {
                let removed = state.items.splice(index, 1)[0];
                state.items.splice(index + action.payload.shift, 0, removed);
            }
        },

        removeByItemIndex: (state, action: PayloadAction<number>) => {
            let index = state.items.findIndex((val: any) => val.index === action.payload);
            if (index !== -1) 
                state.items.splice(index, 1);
        },
        
        removeByProductId: (state, action: PayloadAction<number>) => {
            let index = state.items.reverse().findIndex((val: any) => val.id === action.payload);
            if (index !== -1) {
                index = state.items.length - 1 - index;
                state.items.splice(index, 1);
            }
        }
    }
})

// Action creators are generated for each case reducer function
export const { addProduct, moveProduct, removeByItemIndex, removeByProductId } = cartSlice.actions

export default cartSlice.reducer