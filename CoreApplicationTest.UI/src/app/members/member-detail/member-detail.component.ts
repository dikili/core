import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../_models/User';
import { NgxGalleryOptions, NgxGalleryAnimation } from 'ngx-gallery';
import { NgxGalleryImage } from 'ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs') memberTabs: TabsetComponent;
  user: User;
    galleryOptions: NgxGalleryOptions[];
    galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService, private alertifier: AlertifyService, private router: ActivatedRoute) { }

  ngOnInit() {
   // this.loadUser();
   this.router.data.subscribe(data => {
     this.user = data['user'];
   });

   this.router.queryParams.subscribe(params => {
     const selectedTab = params['tab'];
     this.memberTabs.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
   });

  this.galleryOptions = [
    {
        width: '500px',
        height: '500px',
        thumbnailsColumns: 4,
        imagePercent: 100,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
    },
    ];

    this.galleryImages = this.getImages();

  }
  getImages() {
    const images = [];
    for (let i = 0; i < this.user.photos.length; i++) {
           images.push({
             small: this.user.photos[i].url,
             medium : this.user.photos[i].url,
             large: this.user.photos[i].url,
             description: this.user.photos[i].description
           });
    }

    return images;
   }

   selectTab(tabId: number) {
     this.memberTabs.tabs[tabId].active = true;
   }
  // loadUser() {
  //  this.userService.getUser(+this.router.snapshot.params['id']).subscribe((user: User) => {
  //    this.user = user;
  //  }, error => this.alertifier.error(error));
  // }
}
