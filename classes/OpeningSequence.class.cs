using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceGame
{
    class OpeningSequence
    {
        public static ConsoleKeyInfo Menu()
        {
            string menu = @"                      /\    
                     ----                    
                    /    \               SSSSSS    PPPPPPPP    AAA      CCCCCCCC EEEEEEEEE
                   --------             SSS        PP  PPP    AA AA     CC       EE
                  /        \              SSS      PP   PPP  AA   AA    CC       EE   
                 /          \              SSS     PPPPPPPP AAAAAAAAA   CC       EE
                /            \                SS   PP      AAAAAAAAAAA  CC       EEEEEEEE
               /   --------   \                SS  PP     AA         AA CC       EE
              /    |OOOOOO|    \              SSS  PP     AA         AA CC       EE   
             /     --------     \            SSS   PP     AA         AA CC       EE
            /                    \       SSSSS     PP     AA         AA CCCCCCCC EEEEEEEEE  
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |             DDDDDDDDD     UU      UU DDDDDDDDD     EEEEEEEEE
           |    '  OOOOOOOOO '    |             DD       DD   UU      UU DD       DD   EE        
           |     TTTTTTTTTTTT     |             DD        DD  UU      UU DD        DD  EE     
           |                      |             DD         DD UU      UU DD         DD EE
           |                      |             DD         DD UU      UU DD         DD EEEEEEE   
          / \                    / \            DD        DD  UU      UU DD        DD  EE
         /   \                  /   \           DD       DD   UU      UU DD       DD   EE         
        /     :_______________ :     \          DD      DD    UU      UU DD      DD    EE
       /      / ______________ \      \         DDDDDDDD      UUUUUUUUUU DDDDDDDDD     EEEEEEEEE
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \              /     /                   *with some trading           
          \     \            /     /       
           \     \          /     /   
            \____/          \____/      




    1.  Load Game				2.  New Game				3. Quit";
            Console.WriteLine(menu);
            ConsoleKeyInfo option = Console.ReadKey(false);
            while (option.Key != ConsoleKey.D1 && option.Key != ConsoleKey.D2 && option.Key != ConsoleKey.D3)
            {
                Console.Clear();
                Console.WriteLine(menu);
                option = Console.ReadKey(false);
            }
            return option;
        }
        public static void Animation()
        {
            string one = @"                      /\    
                     ----                    
                    /    \               SSSSSS    PPPPPPPP    AAA      CCCCCCCC EEEEEEEEE
                   --------             SSS        PP  PPP    AA AA     CC       EE
                  /        \              SSS      PP   PPP  AA   AA    CC       EE   
                 /          \              SSS     PPPPPPPP AAAAAAAAA   CC       EE
                /            \                SS   PP      AAAAAAAAAAA  CC       EEEEEEEE
               /   --------   \                SS  PP     AA         AA CC       EE
              /    |OOOOOO|    \              SSS  PP     AA         AA CC       EE   
             /     --------     \            SSS   PP     AA         AA CC       EE
            /                    \       SSSSS     PP     AA         AA CCCCCCCC EEEEEEEEE  
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |             DDDDDDDDD     UU      UU DDDDDDDDD     EEEEEEEEE
           |    '  OOOOOOOOO '    |             DD       DD   UU      UU DD       DD   EE        
           |     TTTTTTTTTTTT     |             DD        DD  UU      UU DD        DD  EE     
           |                      |             DD         DD UU      UU DD         DD EE
           |                      |             DD         DD UU      UU DD         DD EEEEEEE   
          / \                    / \            DD        DD  UU      UU DD        DD  EE
         /   \                  /   \           DD       DD   UU      UU DD       DD   EE         
        /     :_______________ :     \          DD      DD    UU      UU DD      DD    EE
       /      / ______________ \      \         DDDDDDDD      UUUUUUUUUU DDDDDDDDD     EEEEEEEEE
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \              /     /                   *with some trading           
          \     \            /     /       
           \     \          /     /   
            \____/          \____/      ";

        string two = @"                     ----                    
                    /    \      
                   --------             
                  /        \            
                 /          \             
                /            \             
               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  * ,   .  *  /     /                              
          \     \            /     /       
           \     \          /     /   
            \____/          \____/      ";

        string three = @"                    /    \      
                   --------             
                  /        \            
                 /          \             
                /            \             
               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '   ; *  /     /                              
          \     \ * ,   .  * /     /       
           \     \          /     /   
            \____/          \____/      ";

        string four = @"                   --------             
                  /        \            
                 /          \             
                /            \             
               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *    '   *  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/          \____/      ";
        string five = @"                  /        \            
                 /          \             
                /            \             
               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *    '   *  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                   ";
        string six = @"                 /          \             
                /            \             
               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *    '   *  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   ";
        string seven = @"                /            \             
               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *";
        string eight = @"               /   --------   \                
              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *";
        string nine = @"              /    |OOOOOO|    \               
             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  * ";
        string ten = @"             /     --------     \           
            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * ";
        string eleven = @"            /                    \ 
           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *";

        string twelve = @"           |     ------------     |            
           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *";
        string thirteen = @"           |    '  OOOOOOOOO '    |                          
           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *";
        string fourteen = @"           |   '  OOOOOOOOOOO '   |   
           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *";
        string fifteen = @"           |   '  OOOOOOOOOOO '   |           
           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *";
        string sixteen = @"           |    '  OOOOOOOOO '    |             
           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *";
        string seventeen = @"           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *";
        string eighteen = @"           |     TTTTTTTTTTTT     |     
           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *";
        string nineteen = @"           |                      |      
           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *";
        string twenty = @"           |                      |               
          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty1 = @"          / \                    / \            
         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty2 = @"         /   \                  /   \                  
        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty3 = @"        /     :_______________ :     \          
       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty4 = @"       /      / ______________ \      \         
       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty5 = @"       \     /  / |________| \  \     /    
        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty6 = @"        \     \   *''''''''*   /     /  
         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty7 = @"         \     \  *  '    .*  /     /                              
          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty8 = @"          \     \ * '   ;  * /     /       
           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string twenty9 = @"           \     \*,     . */     /   
            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string thirty = @"            \____/* '   .  *\____/      
                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *";
        string thirty1 = @"                 *,   ;  '  *   
                *   .   ,    *
               *  '   .    ;  *
              *     ;   .   ,  *
             * ,   .   '   ;    * 
            *    ,    .   ^   '  *
           * ;   .     ,    ;  .  *
          *    ,    ^    .    ;    *
         * '     .     ,   ;     .  *
        *   ,      ,     .    '      *
       *        .           ,    ;    *
      * ,    ;         -            '  *
     *   '        -        .      ,     *
    * .      ,         ;       -     -   *
   *                                      *
  *                                        *";
            List<string> stf = new List<string>();
            stf.Add(one);
            stf.Add(two);
            stf.Add(three);
            stf.Add(four);
            stf.Add(five);
            stf.Add(six);
            stf.Add(seven);
            stf.Add(eight);
            stf.Add(nine);
            stf.Add(ten);
            stf.Add(eleven);
            stf.Add(twelve);
            stf.Add(thirteen);
            stf.Add(fourteen);
            stf.Add(fifteen);
            stf.Add(sixteen);
            stf.Add(seventeen);
            stf.Add(eighteen);
            stf.Add(nineteen);
            stf.Add(twenty);
            stf.Add(twenty1);
            stf.Add(twenty2);
            stf.Add(twenty3);
            stf.Add(twenty4);
            stf.Add(twenty5);
            stf.Add(twenty6);
            stf.Add(twenty7);
            stf.Add(twenty8);
            stf.Add(twenty9);
            stf.Add(thirty);
            stf.Add(thirty1);

            Console.WriteLine(one);
            
            Console.Clear();
            for (int i = 1; i < stf.Count; i++)
            {
                Console.WriteLine(stf[i]);
                Thread.Sleep(10);
                Console.Clear();
            }
        }
    }
}