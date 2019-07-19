import { Photo } from './Photo';

export interface User {
    id: number;
    userName: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
    l39: string;
    inst: string;
    twit: string;
    face: string;
    businessName: string;
    businessPurpose: string;
    busCategory: string;
    busExplain: string;
    chessLevel: string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
    roles?: string[];
    photos?: Photo[];
}
