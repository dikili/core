import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
{path: 'home', component: HomeComponent},
{path: 'lists', component: ListsComponent, canActivate: [AuthGuard]},
{path: 'members', component: MemberListComponent, canActivate: [AuthGuard]},
{path: 'messages', component: MessagesComponent, canActivate: [AuthGuard]},
{path: '**', redirectTo: 'home', pathMatch: 'full'}
];
