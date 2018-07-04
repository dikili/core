import { User } from './User';

export interface AuthUser {
    tokenString: string;
    mappedUser: User;
}
