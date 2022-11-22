clear 

# SpicyTechsIO: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
# git commit -m "babylon.html"
# git status
# git push  


## SEE : 
## https://github.com/jmake/NovarePotential
##
## git branch -a
##
## git branch BRANCH_NAME
##
## git checkout BRANCH_NAME
##
## git push -u origin BRANCH_NAME
## OR
## git push

GIT_CLONE()
{
  git clone ${REPO} ${BRANCH}
  echo "'${REPO}' downloaded!"  
}


GIT_CHECKOUT() 
{
  git branch -a
  git checkout $BRANCH
  git branch -a
}

echo "WeekNumber:$(date +%U)"

fname=$(date +%b%d%H%M)
fname=$(date +W%U%a%H%M)

rm -rf ${fname}


THEROBOTPROJECT()
{
  REPO="https://jmake@github.com/JungleSlam/TheRobotProject.git"
  BRANCH="Stefans"

  #GIT_CLONE ${REPO}
  cd ${BRANCH}
  GIT_CHECKOUT ${BRANCH}
}


FUTUREGAMES()
{
  REPO="https://jmake@github.com/jmake/FutureGames.git"
  GIT_CLONE ${REPO}
}

FUTUREGAMES




#git clone ${REPO} ${fname}
#echo "'${fname}' downloaded!"  
